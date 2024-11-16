using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Interfaces.Services
{
    public interface IPaymentService
    {
        Task<CustomerBasket?> CreateOrUpdatePaymentIntentId(string BasketId);
        Task<Order> UpdatePaymentIntentToSucceededOrFailed(string PaymentIntentId,bool flag);

    }
}
