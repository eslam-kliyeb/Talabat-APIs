using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class OrderWithPaymentIntentIdSpecifications : BaseSpecifications<Order>
    {
        public OrderWithPaymentIntentIdSpecifications(string PaymentIntentId) : base(O => O.PaymentIntentId == PaymentIntentId)
        {
        }
    }
}
