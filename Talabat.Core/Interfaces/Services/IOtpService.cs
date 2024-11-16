using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Interfaces.Services
{
    public interface IOtpService
    {
        Task UpdateUserOTPCode(AppUser user);
        Task RemoveUserOTPCode(AppUser user);
    }
}
