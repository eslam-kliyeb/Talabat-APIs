using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Core.Interfaces.Repositories.ReadRepository
{
    public interface IGenericReadRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IReadOnlyList<TEntity>> GetAllSpecAsync(ISpecifications<TEntity> Spec);
        Task<TEntity> GetByIdSpecAsync(ISpecifications<TEntity> Spec);
        Task<int> GetCountWithSpecAsync(ISpecifications<TEntity> Spec);
    }
}
