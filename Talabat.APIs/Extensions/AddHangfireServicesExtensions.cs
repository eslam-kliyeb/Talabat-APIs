using Hangfire;

namespace Talabat.APIs.Extensions
{
    public static class AddHangfireServicesExtensions
    {
        public static IServiceCollection AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(config =>
            {
                config.UseSqlServerStorage(configuration.GetConnectionString("SqlConnection"));
            });
            services.AddHangfireServer();
            return services;
        }
    }
}
