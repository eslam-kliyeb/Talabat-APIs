using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Helpers
{
    public class TwoFactorAuthenticationProvider : IUserTwoFactorTokenProvider<AppUser>
    {
        private const int TokenExpireInMinutes = 5;

        public Task<string> GenerateAsync(string purpose, UserManager<AppUser> manager, AppUser user)
        {
            return Task.FromResult(GenerateToken());
        }
        public Task<bool> ValidateAsync(string purpose, string token, UserManager<AppUser> manager, AppUser user)
        {
            var isValid = new DateTime(Convert.ToInt64(token), DateTimeKind.Utc) <= DateTime.UtcNow.AddMinutes(TokenExpireInMinutes);
            return Task.FromResult(isValid);
        }
        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<AppUser> manager, AppUser user)
        {
            return Task.FromResult(true);
        }
        private static string GenerateToken()
        {
            return DateTime.UtcNow.Ticks.ToString();
        }
    }
}
