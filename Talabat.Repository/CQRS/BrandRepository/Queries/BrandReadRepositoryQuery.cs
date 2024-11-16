using MediatR;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.CQRS.BrandRepository.Queries
{
    public record BrandReadRepositoryQuery(DbSet<ProductBrand> ProductBrands, ISpecifications<ProductBrand> Spec) : IRequest<IQueryable<ProductBrand>>;
}
