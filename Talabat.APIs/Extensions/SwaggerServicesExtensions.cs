using Microsoft.OpenApi.Models;

namespace  Talabat.APIs.Extensions
{
    public static class SwaggerServicesExtensions
    {
        public static IServiceCollection AddSwaggerService(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(Option =>
            {
                Option.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
                Option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer",
                });
                Option.AddSecurityRequirement(new OpenApiSecurityRequirement
                      {
                         {
                         new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                          new string[] {}
                         }
                      });
            });
            return services;
        }
    }
}
