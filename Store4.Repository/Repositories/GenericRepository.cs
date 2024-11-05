using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using store4.Core.Repositories.Contract;
using store4.Repository.Data.Contexts;
using Store4.core.Entities;
using Store4.Core.specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Repository.Repositories
{
	public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
	{
		private readonly StoreDbContext _context;

		public GenericRepository(StoreDbContext context)
		{
			_context = context;
		}

		
		public async Task<IEnumerable<TEntity>> GetAllAsync()
		{
			if (typeof(TEntity) == typeof(Product))
			{
				return (IEnumerable<TEntity>)await _context.Products.Include(p => p.Brand).Include(p => p.Type).ToListAsync();
			}
			return await _context.Set<TEntity>().ToListAsync();
		}
		public async Task<TEntity> GetAsync(TKey id)
		{
			if (typeof(TEntity) == typeof(Product))
			{
				return await _context.Products.Include(p => p.Brand).Include(p => p.Type).FirstOrDefaultAsync(p => p.Id == id as int?) as TEntity;
			}
			return await _context.Set<TEntity>().FindAsync(id);

		}
		public async Task AddAsync(TEntity entity)
		{
			await _context.AddAsync(entity);
		}
		public async Task UpdateAsync(TEntity entity)
		{
			 _context.Update(entity);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteAsync(TEntity entity)
		{
			_context.Remove(entity);
			await _context.SaveChangesAsync(); // 
			
		}

		public async Task<IEnumerable<TEntity>> GetAllWithSpecAsync(ISpecifications<TEntity, TKey> Spec)
		{
			//return  await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), Spec).ToListAsync();
			return await ApplySpecifications(Spec).ToListAsync();
		}

		public async Task<TEntity> GetWithSpecAsync(ISpecifications<TEntity, TKey> Spec)
		{
			//return await SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), Spec).FirstOrDefaultAsync();
			return await ApplySpecifications(Spec).FirstOrDefaultAsync();

		}
		private IQueryable<TEntity> ApplySpecifications(ISpecifications<TEntity, TKey> Spec) { 
		
			return SpecificationsEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), Spec);
		}

		public async Task<int> GetCountWithAsync(ISpecifications<TEntity, TKey> Spec)
		{
			return await ApplySpecifications(Spec).CountAsync();
		}
	}
}
