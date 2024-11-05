using store4.Core.Repositories.Contract;
using Store4.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Repositories.Contract
{
	public interface IUnitOfWork
	{
		//for savechange()
		Task<int> CompleteAsync();
		//for generate repository and return
	   IGenericRepository<TEntity,TKey>Repository<TEntity ,TKey>() where TEntity : BaseEntity<TKey> ;
	}
}
