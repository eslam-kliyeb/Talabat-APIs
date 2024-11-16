using System.Linq.Expressions;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces.Specifications.Interface;

namespace Talabat.Core.Interfaces.Specifications.EntitiesSpecifications
{
    public class BaseSpecifications<TEntity> : ISpecifications<TEntity> where TEntity : BaseEntity
    {
        public Expression<Func<TEntity, bool>> Criteria { get; set; }
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>> OrderBy { get ; set; }
        public Expression<Func<TEntity, object>> OrderByDesc { get ; set; }
        //======================================================================
        public int Skip { get; protected set; }
        public int Take { get; protected set; }
        public bool IsPaginated { get; protected set; }
        //======================================================================
        protected void ApplyPagination(int PageSize, int PageIndex)
        {
            IsPaginated = true;
            Take = PageSize;
            Skip = (PageIndex - 1) * PageSize;
        }
        //======================================================================
        //Get ALL
        public BaseSpecifications() { }
        //Get By Id
        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }
    }
}
