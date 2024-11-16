using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces.Repositories.WriteRepository
{
    public interface IGenericWriteRepository<TEntity> where TEntity : BaseEntity
    {
        Task<bool> AddAsync(TEntity Item);
        Task<bool> Update(TEntity Item);
        Task<bool> Delete(TEntity Item);
    }
}
