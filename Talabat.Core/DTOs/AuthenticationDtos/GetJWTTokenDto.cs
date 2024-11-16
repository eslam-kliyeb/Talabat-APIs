namespace Talabat.Core.DTOs.AuthenticationDtos
{
    public class GetJWTTokenDto
    {
        public string TwoFactorToken { get; set; }
        public string OtpCode { get; set; }
        public string Email { get; set; }
    }
}
