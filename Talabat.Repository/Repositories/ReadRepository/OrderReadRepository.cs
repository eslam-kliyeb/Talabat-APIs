using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories.ReadRepository;
using Talabat.Core.Interfaces.Specifications.Interface;
using Talabat.Repository.CQRS.OrderRepository.Queries;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.ReadRepository
{
    public class OrderReadRepository : IOrderReadRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public OrderReadRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<IReadOnlyList<Order>> GetAllSpecAsync(ISpecifications<Order> Spec)
        {
            var result = await _mediator.Send(new OrderReadRepositoryQuery(_dataContext.Orders, Spec));
            return (await result.ToListAsync());
        }

        public async Task<Order> GetByIdSpecAsync(ISpecifications<Order> Spec)
        {
            var result = await _mediator.Send(new OrderReadRepositoryQuery(_dataContext.Orders, Spec));
            return (await result.FirstOrDefaultAsync())!;
        }

        public async Task<int> GetCountWithSpecAsync(ISpecifications<Order> Spec)
        {
            var result = await _mediator.Send(new OrderReadRepositoryQuery(_dataContext.Orders, Spec));
            return (await result.CountAsync())!;
        }
    }
}
