using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.specifications.Orders
{
	public class OrderSpecifications : BaseSpecifications<Order,int>
	{
		public OrderSpecifications(string BuyerEmail) : base(o => o.BuyerEmail == BuyerEmail)
		{
			Incloud.Add(o => o.DeliveryMethod);
			Incloud.Add(o => o.Items);

		}
		public OrderSpecifications(string BuyerEmail, int OrderId) : base(o => o.BuyerEmail == BuyerEmail && o.Id == OrderId)
        {
			Incloud.Add(o => o.DeliveryMethod);
			Incloud.Add(o => o.Items);
		}

    }
}
