using store4.Core.Repositories.Contract;
using store4.Repository.Data.Contexts;
using Store4.core.Entities;
using Store4.Core.Repositories.Contract;
using Store4.Repository.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Repository
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly StoreDbContext _context;
		private Hashtable _repository;
		public UnitOfWork(StoreDbContext context)
		{
			_context = context;
			_repository = new Hashtable();
		}
		public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();


		public  IGenericRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
		{
			var type = typeof(TEntity);
			if (!_repository.Contains(type))
			{   //adding
				var repsitory = new GenericRepository<TEntity, TKey>(_context);
				_repository.Add(type, repsitory);

			}
			//return
			return  _repository[type] as IGenericRepository<TEntity, TKey>;

		}

	}
}
