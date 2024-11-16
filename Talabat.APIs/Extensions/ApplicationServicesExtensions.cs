using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Interfaces.Repositories;
using Talabat.Core.Interfaces.Services;
using Talabat.Repository.CQRS.Startup;
using Talabat.Repository.Data;
using Talabat.Repository.Repositories;
using Talabat.Services;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IBrandService, BrandService>();
            services.AddScoped<ITypeService, TypeService>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICashService, CashService>();
            services.AddTransient<IOtpService, OtpService>();
            services.AddTransient<ImailSettings, EmailService>();
            services.AddTransient<ISMSSettings, SMSService>();
            services.AddMediatR(typeof(StartupCQRS).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles));
            services.AddControllers();
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)
                                                         .SelectMany(p => p.Value.Errors)
                                                         .Select(p => p.ErrorMessage)
                                                         .ToArray();
                    var ValidationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };
            });
            return services;
        }
    }
}
