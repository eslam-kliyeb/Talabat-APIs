using System.Linq.Expressions;
using Talabat.Core.Entities;

namespace Talabat.Core.Interfaces.Specifications.Interface
{
    public interface ISpecifications<TEntity> where TEntity : BaseEntity
    {
        // Sign Fro Property For Where Condition [Where(p=>p.Id==id)]
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        // Sign Fro Property For List Of include [Include(p=>p.Doctor==id)]
        public List<Expression<Func<TEntity, object>>> Includes { get; set; }
        //orderby
        public Expression<Func<TEntity, object>> OrderBy { get;set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get;set; }
        //skip
        public int Skip { get; }
        public int Take { get; }
        public bool IsPaginated { get; }
    }
}
