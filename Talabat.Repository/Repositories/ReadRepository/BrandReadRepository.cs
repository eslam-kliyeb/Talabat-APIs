using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Specifications.Interface;
using Talabat.Repository.CQRS.BrandRepository.Queries;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.ReadRepository
{
    public class BrandReadRepository : IBrandReadRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public BrandReadRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetAllSpecAsync(ISpecifications<ProductBrand> Spec)
        {
            var result = await _mediator.Send(new BrandReadRepositoryQuery(_dataContext.ProductBrands, Spec));
            return (await result.ToListAsync());
        }

        public async Task<ProductBrand> GetByIdSpecAsync(ISpecifications<ProductBrand> Spec)
        {
            var result = await _mediator.Send(new BrandReadRepositoryQuery(_dataContext.ProductBrands, Spec));
            return (await result.FirstOrDefaultAsync())!;
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<ProductBrand> Spec)
        {
            var result = await _mediator.Send(new BrandReadRepositoryQuery(_dataContext.ProductBrands, Spec));
            return (await result.CountAsync());
        }
    }
}
