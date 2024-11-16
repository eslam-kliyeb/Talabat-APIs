using MediatR;
using Talabat.Core.Entities;
using Talabat.Repository.CQRS.TypeRepository.Queries;
using Talabat.Repository.Repositories.Specifications;

namespace Talabat.Repository.CQRS.TypeRepository.Handlers
{
    public class TypeReadRepositoryHandler : IRequestHandler<TypeReadRepositoryQuery, IQueryable<ProductType>>
    {
        public async Task<IQueryable<ProductType>> Handle(TypeReadRepositoryQuery request, CancellationToken cancellationToken)
        {
            var result = SpecificationEvaluator<ProductType>.GetQuery(request.ProductTypes, request.Spec);
            return result;
        }
    }
}
