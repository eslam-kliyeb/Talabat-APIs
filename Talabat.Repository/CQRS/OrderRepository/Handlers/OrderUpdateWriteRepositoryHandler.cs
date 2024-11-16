using MediatR;
using Talabat.Repository.CQRS.OrderRepository.Commands;

namespace Talabat.Repository.CQRS.OrderRepository.Handlers
{
    internal class OrderUpdateWriteRepositoryHandler : IRequestHandler<OrderUpdateWriteRepositoryCommand, bool>
    {
        public async Task<bool> Handle(OrderUpdateWriteRepositoryCommand request, CancellationToken cancellationToken)
        {
            request.DbContext.Orders.Update(request.Order);
            var result = await request.DbContext.SaveChangesAsync();
            return result >= 1 ? true : false;
        }
    }
}
