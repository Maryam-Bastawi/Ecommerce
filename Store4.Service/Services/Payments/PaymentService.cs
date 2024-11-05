using Microsoft.Extensions.Configuration;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities.Orders;
using Store4.Core.Repositories.Contract;
using Store4.Core.Services.Contract;
using Store4.Core.specifications.Orders;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product = Store4.core.Entities.Product;
using Order = Store4.Core.Entities.Orders.Order;

namespace Store4.Service.Services.Payments
{
	public class PaymentService : IPaymentService
	{
		private readonly IBasketService _basketService;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _configuration;

		public PaymentService(IBasketService basketService , IUnitOfWork unitOfWork , IConfiguration configuration)
        {
			_basketService = basketService;
			_unitOfWork = unitOfWork;
			_configuration = configuration;
		}
        public async Task<CoustomerBasketDto> CreateOrUpdatPaymntIntentIdAsync(string BasketId)
		{

			StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
			//get basket
			var basket = await _basketService.GetBasketAsync(BasketId);
			if (basket is null) return null;

			var shippingprice = 0m;
			if (basket.DeliveryMethodId.HasValue)
			{
				var deliverymethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);
				shippingprice = deliverymethod.Cost;
			}
			if(basket.items.Count() > 0)
			{
                foreach (var item in basket.items)
                {
				var product = await	_unitOfWork.Repository<Product, int>().GetAsync(item.id);
					if(item.Price != product.Price)
					{
						item.Price = product.Price;
					}

				}
            }
			var subtotal = basket.items.Sum(i => i.Price * i.Quantity);
			var service = new PaymentIntentService();
			PaymentIntent paymentIntent;
			//service.CreateAsync();
			if (string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				///create
				var options = new PaymentIntentCreateOptions()
				{
					Amount =(long) ( subtotal * 100 + shippingprice *100),
					PaymentMethodTypes = new List<string> { "card"},
					Currency = "usd"
				};

				paymentIntent = await service.CreateAsync(options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;

			}
			else
			{
				////update
				var options = new PaymentIntentUpdateOptions()
				{
					Amount = (long)(subtotal * 100 + shippingprice * 100),
					
				};

				paymentIntent = await service.UpdateAsync(basket.PaymentIntentId,options);
				basket.PaymentIntentId = paymentIntent.Id;
				basket.ClientSecret = paymentIntent.ClientSecret;
			}

			var Basket = await _basketService.UpDateBasketAsync(basket);
			if (Basket is null) return null;

			return Basket;

		}
	



		public async Task<Order> UpdatPaymntIntentForSucceedOrFaildAsync(string PaymntIntentId, bool Flag)
		{
			var spec = new OrderSpecificationWithPatmentIntentId(PaymntIntentId);
			var paymentorder = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
			if (Flag)
			{
				paymentorder.Status = OrderStatus.PaymentReceived;
			}
			else
			{
				paymentorder.Status = OrderStatus.PaymentFailed;

			}
			_unitOfWork.Repository<Order, int>().UpdateAsync(paymentorder);
			await _unitOfWork.CompleteAsync();
			return paymentorder;
		}
	}
}
