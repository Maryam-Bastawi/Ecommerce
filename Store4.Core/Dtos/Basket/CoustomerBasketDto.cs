using Store4.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Dtos.Basket
{
	public class CoustomerBasketDto 
	{
		public string id { get; set; }
		public List<BasketItem> items { get; set; }
		public int? DeliveryMethodId { get; set; }
		public string? PaymentIntentId { get; set; }
		public string? ClientSecret { get; set; }
	}
}
