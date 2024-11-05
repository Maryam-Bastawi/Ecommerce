using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Services.Contract
{
	public interface IOrderService
	{
		Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodid,Address shippingAddress );
		Task <IEnumerable<Order>?> GetOrderForSpecificUser(string BuyerEmail);
		Task <Order?> GetOrderByIdForSpecificUser(string BuyerEmail , int OrderId);

	}
}
