using StackExchange.Redis;
using Store4.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store4.Service.Services.Caches
{
	public class CacheService : ICacheService
	{
		private readonly IConnectionMultiplexer _redis;
		private readonly IDatabase _database;

		public CacheService(IConnectionMultiplexer redis)
        {
			_database = redis.GetDatabase();
		}
        public async Task<string> GetCacheKeyAsync(string Key)
		{
			var chacheResponse = await _database.StringGetAsync(Key);
			if (chacheResponse.IsNullOrEmpty) return null;
			return chacheResponse.ToString();
		}

		public async Task SetCacheKeyAsync(string Key, object Response, TimeSpan ExpireTime)
		{
			if (Response is null) return ;
			var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
			await _database.StringSetAsync(Key, JsonSerializer.Serialize(Response , options), ExpireTime);
		}
	}
}
