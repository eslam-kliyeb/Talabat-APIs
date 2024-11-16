using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Errors;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;
using Talabat.Core.DTOs;
using Talabat.Core.DTOs.AuthenticationDtos;
using Talabat.Core.Entities.EmailSettings;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Entities.Twilio;
using Talabat.Core.Interfaces.Services;

namespace Talabat.APIs.Controllers.Authentication
{
    public class AuthController : ApiBaseController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ImailSettings _imailSettings;
        private readonly ISMSSettings _sMSSettings;
        private readonly IOtpService _otpCodeService;
        private readonly IJwtTokenService _tokenJWTService;
        private readonly ILogger<ExceptionMiddleWare> _logger;
        private readonly IMapper _mapper;
        public AuthController(UserManager<AppUser> userManager,
                              SignInManager<AppUser> signInManager,
                              ImailSettings imailSettings,
                              ISMSSettings sMSSettings,
                              IOtpService otpCodeService,
                              IJwtTokenService tokenJWTService,
                              ILogger<ExceptionMiddleWare> logger,
                              IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imailSettings = imailSettings;
            _sMSSettings = sMSSettings;
            _otpCodeService = otpCodeService;
            _tokenJWTService = tokenJWTService;
            _logger = logger;
            _mapper = mapper;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult<RegisterResDto>> Register(RegisterDto model)
        {

            if (CheckEmailExists(model.Email).Result.Value)
            {
                return BadRequest(new ApiResponse(400,"Email Is Already Use"));
            }
            var User = new AppUser()
            {
                DisplayName = model.DisplayName,
                Email = model.Email,
                UserName = model.Email.Split('@')[0],
                PhoneNumber = model.PhoneNumber,

            };
            var result = await _userManager.CreateAsync(User, model.Password);
            if (!result.Succeeded)
                return BadRequest(new ApiResponse(400, "User creation failed.", result.Errors.FirstOrDefault()?.Description));

            var ReturnedUser = new RegisterResDto()
            {
                DisplayName = User.DisplayName,
                Email = User.Email,
            };
            return Ok(ReturnedUser);
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<ActionResult<GetJWTTokenDto>> Login(LoginDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null)
                return Unauthorized(new ApiResponse(401, "User not found."));

            var Result = await _signInManager.CheckPasswordSignInAsync(User, model.Password, false);
            if (!Result.Succeeded)
                return Unauthorized(new ApiResponse(401, "Invalid credentials. Please check your email and password."));

            await _otpCodeService.UpdateUserOTPCode(User);
            try
            {
                var EmailOtp = new Email
                {
                    To = User.Email,
                    Subject = "Talabat OTP",
                    Body = User.OTPCode,
                };
                var SMSOtp = new SMS
                {
                    mobileNumber = $"+2{User.PhoneNumber}",
                    body = $"Talabat OTP {User.OTPCode}"
                };
                BackgroundJob.Enqueue(() => _imailSettings.SendMail(EmailOtp));
                BackgroundJob.Enqueue(() => _sMSSettings.SendSMS(SMSOtp));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send OTP via Email or SMS.");
                return StatusCode(500, new ApiResponse(500, "An error occurred while sending the OTP."));
            }
            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(User, "SystemUserTokenProvider");

            var ResultLogin = new GetJWTTokenDto()
            {
                OtpCode = User.OTPCode,
                TwoFactorToken = twoFactorToken,
                Email = User.Email
            };
            return Ok(ResultLogin);
        }
        [AllowAnonymous]
        [HttpPost("GetJWTToken")]
        public async Task<ActionResult<UserDto>> GetJWTToken([FromBody] GetJWTTokenDto model)
        {
            var User = await _userManager.FindByEmailAsync(model.Email);
            if (User is null) return Unauthorized(new ApiResponse(401, "User not found."));

            var verifyTwoFactorTokenResult = await _userManager.VerifyTwoFactorTokenAsync(User, "SystemUserTokenProvider", model.TwoFactorToken);

            if (!verifyTwoFactorTokenResult)
                return BadRequest(new ApiResponse(400, "Two-factor authentication failed."));

            if (User.OTPCode != model.OtpCode)
                return BadRequest(new ApiResponse(400, "Invalid OTP code."));

            if (DateTime.UtcNow >= User.OTPExpiry)
                return BadRequest(new ApiResponse(400, "OTP code has expired. Please request a new one."));

            var jwtToken = await _tokenJWTService.GetToken(User);
            await _otpCodeService.RemoveUserOTPCode(User);

            return Ok(new UserDto
            {
                Token = jwtToken,
                Email = User.Email,
                DisplayName = User.DisplayName,
            });
        }
        [Authorize]
        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var Email = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(Email);
            var result = new UserDto()
            {
                Email = user.Email,
                DisplayName = user.DisplayName,
                Token = await _tokenJWTService.GetToken(user)
            };
            return Ok(result);
        }
        [Authorize]
        [HttpGet("GetAddressCurrentUser")]
        public async Task<ActionResult<AddressDto>> GetAddressCurrentUser()
        {
            var user = await _userManager.FindUserWithAdressAsync(User);
            var Address = user.Address;
            var result = _mapper.Map<AddressDto>(Address);
            if (result == null) return NotFound(new ApiResponse(404, "Address not found for the user"));
            return Ok(result);
        }
        [Authorize]
        [HttpPost("UpdateAndCreateAddressCurrentUser")]
        public async Task<ActionResult<AddressDto>> UpdateAddressCurrentUser([FromBody] AddressDto addressDto)
        {
            var user = await _userManager.FindUserWithAdressAsync(User);
            var Address = _mapper.Map<Address>(addressDto);
            user.Address = Address;
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) return NotFound(new ApiResponse(400, "Address not Update"));
            return Ok(addressDto);
        }
        [HttpGet("emailExists")]
        public async Task<ActionResult<bool>> CheckEmailExists(string Email)
        {
            return await _userManager.FindByEmailAsync(Email) is not null;
        }
    }
}
