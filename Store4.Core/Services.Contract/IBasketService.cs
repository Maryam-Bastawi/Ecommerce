using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Dtos.Basket
{
	public interface IBasketService
	{
		Task<CoustomerBasketDto?> GetBasketAsync(string basketid);
		Task<CoustomerBasketDto?> UpDateBasketAsync(CoustomerBasketDto BasketDto);
		Task<bool> DeleteBasketAsync(string basketid);

	}
}
