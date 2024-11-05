using Store4.core.Entities;
using Store4.Core.specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace store4.Core.Repositories.Contract
{
	public interface IGenericRepository<TEntity , TKey>where TEntity : BaseEntity<TKey>
	{
		Task<IEnumerable<TEntity>> GetAllAsync();
		Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity,TKey>Spec);
		Task<TEntity> GetAsync(TKey id);
		Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> Spec);
		Task<int> GetCountWithAsync(ISpecifications<TEntity, TKey> Spec);
		Task AddAsync(TEntity entity);

		Task UpdateAsync(TEntity entity);
		Task DeleteAsync(TEntity entity);

	}
}
