using AutoMapper;
using Talabat.Core.DTOs;
using Talabat.Core.Entities.Order_Aggregate;
namespace Talabat.APIs.Helpers
{
    public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
    {
        private readonly IConfiguration _configuration;
        public OrderItemPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrWhiteSpace(source.Product.PictureUrl))
            {
                return $"{_configuration["BaseUrl"]}{source.Product.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
