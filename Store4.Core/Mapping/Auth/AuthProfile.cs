using AutoMapper;
using Store4.Core.Dtos.Auth;
using Store4.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

namespace Store4.Core.Mapping.Auth
{
	public class AuthProfile : Profile
	{
        public AuthProfile()
        {
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
