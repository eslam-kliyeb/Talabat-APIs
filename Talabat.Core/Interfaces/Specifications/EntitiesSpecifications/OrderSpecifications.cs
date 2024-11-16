using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class OrderSpecifications : BaseSpecifications<Order>
    {
        public OrderSpecifications(string email,int Id) : base(O => O.BuyerEmail == email && O.Id==Id)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
        }
        public OrderSpecifications(string email) :base(O=>O.BuyerEmail==email)
        {
            Includes.Add(O => O.DeliveryMethod);
            Includes.Add(O => O.Items);
            OrderByDesc = O => O.OrderDate;
        }
    }
}
