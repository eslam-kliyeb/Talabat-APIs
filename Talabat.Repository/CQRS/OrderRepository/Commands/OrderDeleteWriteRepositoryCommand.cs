using MediatR;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Repository.Data;

namespace Talabat.Repository.CQRS.OrderRepository.Commands
{
    public record OrderDeleteWriteRepositoryCommand(ApplicationDbContext DbContext, Order Order) : IRequest<bool>;
}
