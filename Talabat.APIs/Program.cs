using Hangfire;
using Talabat.APIs.Extensions;
using Talabat.APIs.Middlewares;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddApplicationServices();
            builder.Services.AddDataBasesConnectionsServices(builder.Configuration);
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddHangfireServices(builder.Configuration);
            builder.Services.AddEndpointsApiExplorer();
            //builder.Services.AddSwaggerGen
            builder.Services.AddSwaggerService();
            #endregion
            var app = builder.Build();
            await DbInitializer.InitializeDbAsync(app);
            #region Configure the HTTP request pipeline.
            app.UseMiddleware<RateLimitingMiddleware>();
            app.UseMiddleware<ExceptionMiddleWare>();
            //AddHangfireServicesExtensions
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseStatusCodePagesWithRedirects("/errors/{0}");
            app.UseHttpsRedirection();

            app.UseHangfireDashboard("/hangfire");

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.MapControllers();
            #endregion
            app.Run();
        }
    }
}
