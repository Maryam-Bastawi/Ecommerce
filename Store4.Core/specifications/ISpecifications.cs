using System.Linq.Expressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store4.core.Entities;

namespace Store4.Core.specifications
{
	public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		public Expression<Func<TEntity, bool>> Criteria { get; set; }
		public List<Expression<Func<TEntity, object>>> Incloud { get; set; }
		public Expression<Func<TEntity, object>> OrderBy { get; set; }
		public Expression<Func<TEntity, object>> OrderByDesc { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public bool IsPaginationEnabled { get; set; }
    }
}
