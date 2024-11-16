using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Identity;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Identity;

namespace Talabat.APIs.Extensions
{
    public static class DbInitializer
    {
        public static async Task InitializeDbAsync(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {     //Group Of Services LifeTime Scoped
                var service = scope.ServiceProvider;
                //Services Its Self
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var Context = service.GetRequiredService<ApplicationDbContext>();
                    var userManager = service.GetRequiredService<UserManager<AppUser>>();
                    //Create DB if it does not exist
                    if ((await Context.Database.GetPendingMigrationsAsync()).Any())
                         await Context.Database.MigrateAsync();
                    //Apply Seeding
                    await ContextSeed.SeedAsync(Context);
                    await AppIdentityDbContextSeed.SeedUserAsync(userManager, Context);
                }
                catch (Exception ex)
                {
                    var logger = loggerFactory.CreateLogger<Program>();
                    logger.LogError(ex.Message,"An Error Occured During Appling The Migration");
                }
            }
        }
    }
}
