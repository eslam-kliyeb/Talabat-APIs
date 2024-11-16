using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.CQRS.TypeRepository.Queries
{
    public record TypeReadRepositoryQuery(DbSet<ProductType> ProductTypes, ISpecifications<ProductType> Spec) : IRequest<IQueryable<ProductType>>;
}
