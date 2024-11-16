using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using Talabat.Core.Entities.EmailSettings;
using Talabat.Core.Entities.Twilio;
using Talabat.Repository.Data;

namespace Talabat.APIs.Extensions
{
    public static class DataBasesConnectionsServicesExtensions
    {
        public static IServiceCollection AddDataBasesConnectionsServices(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(sql => {
                sql.UseSqlServer(configuration.GetConnectionString("SqlConnection"));
            });
            services.AddSingleton<IConnectionMultiplexer>(Options =>
            {
                var Connection = configuration.GetConnectionString("RedisConnection");
                return ConnectionMultiplexer.Connect(Connection);
            });
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            services.Configure<TwilioSettings>(configuration.GetSection("Twilio"));
            return services;
        }
    }
}
