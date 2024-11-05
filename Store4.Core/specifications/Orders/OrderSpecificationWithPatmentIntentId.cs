using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.specifications.Orders
{
    public class OrderSpecificationWithPatmentIntentId : BaseSpecifications<Order,int>
    {
        public OrderSpecificationWithPatmentIntentId(string paymentIntentId): base(o => o.PaymentIntentId == paymentIntentId)
        {
            Incloud.Add(o => o.DeliveryMethod);
            Incloud.Add(o => o.Items);
        }
    }
}
