using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.CQRS.OrderRepository.Queries
{
    public record OrderReadRepositoryQuery(DbSet<Order> Orders, ISpecifications<Order> Spec) :IRequest<IQueryable<Order>>;
}
