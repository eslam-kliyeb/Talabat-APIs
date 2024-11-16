using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Data.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, ApplicationDbContext applicationDb)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser
                {
                    DisplayName = "Islam Kliyeb",
                    Email = "eslamklyep2019@gmail.com",
                    UserName = "eslamklyep2019",
                    PhoneNumber = "01121130319"
                };
                await userManager.CreateAsync(User, "Pa$$w0rd");
            }
        }
    }
}
