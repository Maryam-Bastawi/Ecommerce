using Store4.core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Store4.Core.Entities.Orders;


namespace Store4.Core.Entities.Orders
{
	public class Order : BaseEntity<int>
	{
        public Order()
        {
            
        }
        public Order(string buyerEmail,  Address shippingAddress, DeliveryMethod deliveryMethod,
			 ICollection<OrderItems> items, decimal subTotal, string paymentIntentId)
		{
			BuyerEmail = buyerEmail;
			
			ShippingAddress = shippingAddress;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
			PaymentIntentId = paymentIntentId;
		}

		public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; }
        public Address ShippingAddress { get; set; }
		public int DeliveryMethodId { get; set; } //fk
		public DeliveryMethod DeliveryMethod { get; set; }
        public ICollection<OrderItems> Items { get; set; }
        public decimal SubTotal { get; set; }
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;
        public string PaymentIntentId { get; set; }
    }
}
