using MediatR;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repository.CQRS.DeliveryMethodRepository.Queries;
using Talabat.Repository.Repositories.Specifications;

namespace Talabat.Repository.CQRS.DeliveryMethodRepository.Handlers
{
    public class DeliveryMethodReadRepositoryHandler : IRequestHandler<DeliveryMethodReadRepositoryQuery, IQueryable<DeliveryMethod>>
    {
        public async Task<IQueryable<DeliveryMethod>> Handle(DeliveryMethodReadRepositoryQuery request, CancellationToken cancellationToken)
        {
            var result = SpecificationEvaluator<DeliveryMethod>.GetQuery(request.DeliveryMethods, request.Spec);
            return result;
        }
    }
}
