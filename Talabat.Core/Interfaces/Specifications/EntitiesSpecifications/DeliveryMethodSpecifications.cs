using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class DeliveryMethodSpecifications : BaseSpecifications<DeliveryMethod>
    {
        public DeliveryMethodSpecifications() : base()
        {
        }
        public DeliveryMethodSpecifications(int id) : base(d => d.Id == id)
        {
        }
    }
}
