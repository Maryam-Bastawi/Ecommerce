using Store4.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.specifications
{
	public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Expression<Func<TEntity, bool>> Criteria { get; set; } //null
		public List<Expression<Func<TEntity, object>>> Incloud { get; set; } = new List<Expression<Func<TEntity, object>>>();
		public Expression<Func<TEntity, object>> OrderBy { get; set; } = null;
		public Expression<Func<TEntity, object>> OrderByDesc { get; set; } = null;
		public int skip { get ; set ; }
		public int take { get ; set ; }
		public bool IsPaginationEnabled { get ; set ; }

		public BaseSpecifications(Expression<Func<TEntity, bool>> Exception)
        {
			Criteria = Exception;
			


		}
        public BaseSpecifications()
        {
            
        }
		public void AddOrderBy(Expression<Func<TEntity, object>> expression)
		{
			OrderBy = expression;
		}
		public void AddOrdrByDesc(Expression<Func<TEntity, object>> expression)
		{
			OrderByDesc = expression;
		}
		public void ApplyPagination(int Skip , int Take)
		{
			IsPaginationEnabled = true;
			skip = Skip;
			take = Take;

		}
    }


}

