using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.CQRS.ProductRepository.Queries
{
    public record ProductReadRepositoryQuery(DbSet<Product> Products, ISpecifications<Product> Spec) : IRequest<IQueryable<Product>>;
}
