using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities.Order_Aggregate;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.CQRS.DeliveryMethodRepository.Queries
{
    public record DeliveryMethodReadRepositoryQuery(DbSet<DeliveryMethod> DeliveryMethods, ISpecifications<DeliveryMethod> Spec) : IRequest<IQueryable<DeliveryMethod>>;
}
