using StackExchange.Redis;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities;
using Store4.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store4.Repository.Repositories
{
	public class BasketRepsitory : IBasketRepository
	{
		private readonly IDatabase _database;

		public BasketRepsitory(IConnectionMultiplexer redis)
        {
		   _database =redis.GetDatabase();
        }
        public async Task<bool> DeleteBasketAsync(string BasketId)
		{
			return await _database.KeyDeleteAsync(BasketId);
  
		}

		public async Task<CoustomerBasket?> GetBasketAsync(string BasketId)
		{
			var Basket =  await _database.StringGetAsync(BasketId);

			return Basket.IsNullOrEmpty ? null : JsonSerializer.Deserialize<CoustomerBasket>(Basket);
		}

		public async Task<CoustomerBasket?> UpDateBasketAsync(CoustomerBasket basket)
		{
			var setupdate = await _database.StringSetAsync(basket.id, JsonSerializer.Serialize(basket), TimeSpan.FromDays(30));
			if (setupdate is false) return null;
			return await GetBasketAsync(basket.id);
		}
	}
}
