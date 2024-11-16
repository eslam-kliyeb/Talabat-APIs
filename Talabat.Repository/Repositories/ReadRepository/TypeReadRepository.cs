using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Specifications.Interface;
using Talabat.Repository.CQRS.TypeRepository.Queries;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.ReadRepository
{
    public class TypeReadRepository : ITypeReadRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public TypeReadRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<IReadOnlyList<ProductType>> GetAllSpecAsync(ISpecifications<ProductType> Spec)
        {
            var result = await _mediator.Send(new TypeReadRepositoryQuery(_dataContext.ProductTypes, Spec));
            return (await result.ToListAsync());
        }

        public async Task<ProductType> GetByIdSpecAsync(ISpecifications<ProductType> Spec)
        {
            var result = await _mediator.Send(new TypeReadRepositoryQuery(_dataContext.ProductTypes, Spec));
            return (await result.FirstOrDefaultAsync())!;
        }
        public async Task<int> GetCountWithSpecAsync(ISpecifications<ProductType> Spec)
        {
            var result = await _mediator.Send(new TypeReadRepositoryQuery(_dataContext.ProductTypes, Spec));
            return (await result.CountAsync());
        }
    }
}
