using MediatR;
using Talabat.Repository.CQRS.OrderRepository.Commands;

namespace Talabat.Repository.CQRS.OrderRepository.Handlers
{
    public class OrderAddWriteRepositoryHandler : IRequestHandler<OrderAddWriteRepositoryCommand, bool>
    {
        public async Task<bool> Handle(OrderAddWriteRepositoryCommand request, CancellationToken cancellationToken)
        {
            await request.DbContext.Orders.AddAsync(request.Order);
            var result = await request.DbContext.SaveChangesAsync();
            return result>=1?true:false; 
        }
    }
}
