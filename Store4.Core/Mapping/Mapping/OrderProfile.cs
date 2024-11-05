using AutoMapper;
using Microsoft.Extensions.Configuration;
using Store4.Core.Dtos.Auth;
using Store4.Core.Dtos.Orders;
using Store4.Core.Entities.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Mapping.Mapping
{
	public class OrderProfile : Profile
	{

		public OrderProfile(IConfiguration configuration)
        {
            CreateMap<Order, OrderToReturnDto>()
            .ForMember(d => d.DeliveryMethod, Options => Options.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.DeliveryMethodCost, Options => Options.MapFrom(s => s.DeliveryMethod.Cost));
			CreateMap<Address, AddressDto>().ReverseMap();

			CreateMap<OrderItems, OrderItemsDto>()
			.ForMember(d => d.ProductId, Options => Options.MapFrom(s => s.Product.ProductId))
			.ForMember(d => d.ProductName, Options => Options.MapFrom(s => s.Product.ProductName))
			.ForMember(d => d.PictureUrl, Options => Options.MapFrom(s => $"{configuration["baseurl"]}{s.Product.ProductId}"));
			CreateMap<DeliveryMethod, DeliveryMethodDto>().ReverseMap();

		}
	}
}
