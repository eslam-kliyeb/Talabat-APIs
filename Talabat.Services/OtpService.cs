using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces.Services;

namespace Talabat.Services
{
    public class OtpService : IOtpService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;

        public OtpService(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task RemoveUserOTPCode(AppUser user)
        {
            user.OTPCode = null;
            user.OTPExpiry = null;
            await _userManager.UpdateAsync(user);
        }
        public async Task UpdateUserOTPCode(AppUser user)
        {
            int.TryParse(_configuration["OTPExpiryMinutes"], out int otpExpiryMinutes);
            var random = new Random();
            var code = random.Next(100000, 999999);
            user.OTPCode = code.ToString();
            user.OTPExpiry = DateTime.UtcNow.AddMinutes(otpExpiryMinutes);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).FirstOrDefault());
            }
        }
    }
}
