using MediatR;
using Talabat.Core.Entities;
using Talabat.Repository.CQRS.BrandRepository.Queries;
using Talabat.Repository.Repositories.Specifications;

namespace Talabat.Repository.CQRS.BrandRepository.Handlers
{
    public class BrandReadRepositoryHandler : IRequestHandler<BrandReadRepositoryQuery, IQueryable<ProductBrand>>
    {
        public async Task<IQueryable<ProductBrand>> Handle(BrandReadRepositoryQuery request, CancellationToken cancellationToken)
        {
            var result = SpecificationEvaluator<ProductBrand>.GetQuery(request.ProductBrands, request.Spec);
            return result;
        }
    }
}
