using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Services.Contract
{
	public interface ICacheService
	{
		Task SetCacheKeyAsync(string Key, object value, TimeSpan ExpireTime);
		Task<string> GetCacheKeyAsync(string Key);
	}
}
