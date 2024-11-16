using MediatR;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repository.CQRS.OrderRepository.Queries;
using Talabat.Repository.Repositories.Specifications;

namespace Talabat.Repository.CQRS.OrderRepository.Handlers
{
    public class OrderReadRepositoryHandler : IRequestHandler<OrderReadRepositoryQuery, IQueryable<Order>>
    {
        public async Task<IQueryable<Order>> Handle(OrderReadRepositoryQuery request, CancellationToken cancellationToken)
        {
            var result = SpecificationEvaluator<Order>.GetQuery(request.Orders, request.Spec);
            return result;
        }
    }
}
