using Store4.core.Entities;
using Store4.Core.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.specifications.Products
{
	public class ProductWithCountSpecifications : BaseSpecifications<Product, int>
	{

		public ProductWithCountSpecifications(ProductSpecParams ProductSpec) : base(

	     p => (string.IsNullOrEmpty(ProductSpec.Search) || (p.Name.ToLower().Contains(ProductSpec.Search)) && 
		 (!ProductSpec.BrandId.HasValue || ProductSpec.BrandId == p.BrandId) && 
		 (ProductSpec.TypeId.HasValue || ProductSpec.TypeId == p.TypeId)))
		{
		}

	}
}
