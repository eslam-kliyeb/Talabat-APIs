using MediatR;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Repositories.WriteRepository;
using Talabat.Repository.CQRS.OrderRepository.Commands;
using Talabat.Repository.Data;

namespace Talabat.Repository.Repositories.WriteRepository
{
    public class OrderWriteRepository : IOrderWriteRepository
    {
        private readonly IMediator _mediator;
        private readonly ApplicationDbContext _dataContext;
        public OrderWriteRepository(ApplicationDbContext dataContext, IMediator mediator)
        {
            _mediator = mediator;
            _dataContext = dataContext;
        }
        public async Task<bool> AddAsync(Order Item)
        {
           var result = await _mediator.Send(new OrderAddWriteRepositoryCommand(_dataContext, Item));
           return result;
        }

        public async Task<bool> Delete(Order Item)
        {
            var result = await _mediator.Send(new OrderDeleteWriteRepositoryCommand(_dataContext, Item));
            return result;
        }

        public async Task<bool> Update(Order Item)
        {
            var result = await _mediator.Send(new OrderUpdateWriteRepositoryCommand(_dataContext, Item));
            return result;
        }
    }
}
