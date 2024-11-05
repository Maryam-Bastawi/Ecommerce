using Store4.Core.Dtos.Auth;
using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Dtos.Orders
{
	public class OrderToReturnDto
	{
		public int Id { get; set; }

		public string BuyerEmail { get; set; }
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
		public string Status { get; set; }
		public AddressDto ShippingAddress { get; set; }
		public string DeliveryMethod { get; set; }
		public decimal DeliveryMethodCost { get; set; }
		public ICollection<OrderItemsDto> Items { get; set; }
		public decimal SubTotal { get; set; }
		public decimal Total { get; set; }
		public string? PaymentIntentId { get; set; } = string.Empty;
	}
}
