using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Store4.core.Entities;
using Store4.Core.Dtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Mapping.Products
{
    public class ProductProfile : Profile

    {
        public ProductProfile(IConfiguration configuration)
        {
            CreateMap<Product, ProductDto>()
            .ForMember(d => d.BrandName, Options => Options.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.TypeName, Options => Options.MapFrom(s => s.Type.Name))
            .ForMember(d => d.PictureUrl, Options => Options.MapFrom(s => $"{configuration["baseurl"]}/{ s.PictureUrl}"));
            CreateMap<ProductBrand, TypeBrandDto>();
            CreateMap<ProductType, TypeBrandDto>();
        }
    }
}
