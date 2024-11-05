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
	public class OrdersItemsConfigurations : IEntityTypeConfiguration<OrderItems>
	{
		public void Configure(EntityTypeBuilder<OrderItems> builder)
		{
			builder.OwnsOne(o => o.Product, p => p.WithOwner());
			builder.Property(O => O.Price).HasColumnType("decimal(18,2)");

		}
	}
}
