using Talabat.Core.DTOs;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Interfaces.Services
{
    public interface IOrderService
    {
        Task<OrderResDto?> CreateOrderAsync(string BuyerEmail, string BasketId, int DeliveryMethodId, Address ShippingAddress);
        Task<IReadOnlyList<OrderResDto>> GetOrdersForSpecificUser(string BuyerEmail);
        Task<OrderResDto> GetOrdersByIdForSpecificUser(string BuyerEmail,int OrderId);
    }
}
