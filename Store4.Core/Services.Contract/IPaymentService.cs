using Store4.Core.Dtos.Basket;
using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Services.Contract
{
	public interface IPaymentService
	{
		Task<CoustomerBasketDto> CreateOrUpdatPaymntIntentIdAsync(string BasketId);
		Task<Order> UpdatPaymntIntentForSucceedOrFaildAsync(string PaymntIntentId , bool Flag);
	}
}
