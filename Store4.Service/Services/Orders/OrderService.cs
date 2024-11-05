using Store4.core.Entities;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities.Orders;
using Store4.Core.Repositories.Contract;
using Store4.Core.Services.Contract;
using Store4.Core.specifications.Orders;
using Store4.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Service.Services.Orders
{
	public class OrderService : IOrderService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IBasketService _basketService;
		private readonly IPaymentService _paymentService;

		public OrderService(IUnitOfWork unitOfWork , IBasketService basketService , IPaymentService paymentService)
        {
			_unitOfWork = unitOfWork;
			_basketService = basketService;
			_paymentService = paymentService;
		}
        public async Task<Order> CreateOrderAsync(string BuyerEmail, string BasketId, int deliveryMethodid, Address shippingAddress)
		{
			var basket = await _basketService.GetBasketAsync(BasketId);
			if (basket is null) return null;
			var orderitems = new List<OrderItems>();
			if (basket.items.Count() > 0)
			{
                foreach (var item in basket.items)
                {
					var product =await _unitOfWork.Repository<Product,int>().GetAsync(item.id) ;
					var productOrderitem = new ProductItemOrder(product.Id , product.Name , product.PictureUrl);
					var orderitem = new OrderItems(productOrderitem, product.Price, item.Quantity);
					orderitems.Add(orderitem);

				}
            }
				var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod, int>().GetAsync(deliveryMethodid);
		     	var subtotal = orderitems.Sum(p => p.Price * p.Quantity);
			//todo
			if (! string.IsNullOrEmpty(basket.PaymentIntentId))
			{
				var spec = new OrderSpecificationWithPatmentIntentId(basket.PaymentIntentId);
				var ExOrder = await _unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
				await _unitOfWork.Repository<Order, int>().DeleteAsync(ExOrder);

			}

		 var basketdto = await _paymentService.CreateOrUpdatPaymntIntentIdAsync(BasketId);
			    var order =  new Order(BuyerEmail, shippingAddress, deliveryMethod, orderitems, subtotal, basketdto.PaymentIntentId);

			await _unitOfWork.Repository<Order, int>().AddAsync(order);

			var result = await _unitOfWork.CompleteAsync();
		     	if (result <= 0) return null;
			    return order;
		}
		public Task<IEnumerable<Order>?> GetOrderForSpecificUser(string BuyerEmail)
		{
			var spec = new OrderSpecifications(BuyerEmail);
			var order = _unitOfWork.Repository<Order, int>().GetAllWithSpecAsync(spec);
			if (order is null) return null;
			return order;
		}
		public Task<Order?> GetOrderByIdForSpecificUser(string BuyerEmail, int OrderId)
		{
			var spec = new OrderSpecifications(BuyerEmail, OrderId);
		   var order =	_unitOfWork.Repository<Order, int>().GetWithSpecAsync(spec);
			if (order is null) return null;
			return order;
		}
	}
}
