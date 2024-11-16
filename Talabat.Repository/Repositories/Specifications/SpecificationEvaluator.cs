using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Repository.Repositories.Specifications
{
    public static class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        //fun to build Query
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecifications<TEntity> specifications)
        {
            var query = inputQuery;  // _dataContext.Set<TEntity>
            if (specifications.Criteria is not null)
            {
                query=query.Where(specifications.Criteria);
            }
            if (specifications.OrderBy is not null)
            {
                query = query.OrderBy(specifications.OrderBy);
            }
            if (specifications.OrderByDesc is not null)
            {
                query = query.OrderByDescending(specifications.OrderByDesc);
            }
            if (specifications.IsPaginated)
            {
                query = query.Skip(specifications.Skip).Take(specifications.Take);
            }
            foreach (var include in specifications.Includes) 
            {
                query=query.Include(include);
            }
            return query;
        }
    }
}
