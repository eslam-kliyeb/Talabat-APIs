using MediatR;
using Talabat.Repository.CQRS.OrderRepository.Commands;

namespace Talabat.Repository.CQRS.OrderRepository.Handlers
{
    public class OrderDeleteWriteRepositoryHandler : IRequestHandler<OrderDeleteWriteRepositoryCommand, bool>
    {
        public async Task<bool> Handle(OrderDeleteWriteRepositoryCommand request, CancellationToken cancellationToken)
        {
            request.DbContext.Orders.Remove(request.Order);
            var result = await request.DbContext.SaveChangesAsync();
            return result >= 1 ? true : false;
        }
    }
}
