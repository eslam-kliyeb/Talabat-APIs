using Talabat.Core.Entities.Identity;

namespace Talabat.Core.Interfaces.Services
{
    public interface IJwtTokenService
    {
        Task<string> GetToken(AppUser user);
    }
}
