using AutoMapper;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Mapping.Baskets
{
	public class BasketProfile : Profile
	{
		public BasketProfile()
		{
			CreateMap<CoustomerBasket, CoustomerBasketDto>().ReverseMap();

		}
	}
}
