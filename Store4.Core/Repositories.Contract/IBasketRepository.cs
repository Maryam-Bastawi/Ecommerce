using Store4.Core.Dtos.Basket;
using Store4.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Repositories.Contract
{
	public interface IBasketRepository
	{
		Task<CoustomerBasket?> GetBasketAsync(string BasketId);
		Task<CoustomerBasket?>	UpDateBasketAsync(CoustomerBasket basket);
		Task<bool>DeleteBasketAsync(string BasketId);
	}
}
