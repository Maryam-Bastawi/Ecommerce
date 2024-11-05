using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Repository.Data.Configurations
{
	public class OrdersConfigurations : IEntityTypeConfiguration<Order>
	{
		public void Configure(EntityTypeBuilder<Order> builder)
		{
			builder.Property(O => O.SubTotal).HasColumnType("decimal(18,2)");
			builder.Property(O => O.Status).HasConversion(os => os.ToString() ,  os => (OrderStatus)Enum.Parse(typeof(OrderStatus),os));
			builder.OwnsOne(O => O.ShippingAddress, SA => SA.WithOwner());
			builder.HasOne(O => O.DeliveryMethod).WithMany().HasForeignKey(o => o.DeliveryMethodId);

		}
	}
}
