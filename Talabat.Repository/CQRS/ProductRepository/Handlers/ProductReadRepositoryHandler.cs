using MediatR;
using Talabat.Core.Entities;
using Talabat.Repository.CQRS.ProductRepository.Queries;
using Talabat.Repository.Repositories.Specifications;

namespace Talabat.Repository.CQRS.ProductRepository.Handlers
{
    public class ProductReadRepositoryHandler : IRequestHandler<ProductReadRepositoryQuery, IQueryable<Product>>
    {
        public async Task<IQueryable<Product>> Handle(ProductReadRepositoryQuery request, CancellationToken cancellationToken)
        {
            var result = SpecificationEvaluator<Product>.GetQuery(request.Products, request.Spec);
            return  result;
        }
    }
}
