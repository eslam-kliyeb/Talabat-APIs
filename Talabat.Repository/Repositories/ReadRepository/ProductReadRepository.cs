using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Specifications.Interface;
using Talabat.Repository.CQRS.ProductRepository.Queries;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.ReadRepository
{
    public class ProductReadRepository : IProductReadRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public ProductReadRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<IReadOnlyList<Product>> GetAllSpecAsync(ISpecifications<Product> Spec)
        {
            var result = await  _mediator.Send(new ProductReadRepositoryQuery(_dataContext.Products, Spec));
            return (await result.ToListAsync());
        }
        public async Task<Product> GetByIdSpecAsync(ISpecifications<Product> Spec)
        {
            var result = await _mediator.Send(new ProductReadRepositoryQuery(_dataContext.Products, Spec));
            return (await result.FirstOrDefaultAsync())!;
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<Product> Spec)
        {
            var result = await _mediator.Send(new ProductReadRepositoryQuery(_dataContext.Products, Spec));
            return (await result.CountAsync());
        }
    }
}
