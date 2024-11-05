using Store4.core.Entities;
using Store4.Core.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.specifications.Products
{
	public class ProductSpecifications : BaseSpecifications<Product,int>
	{
        public ProductSpecifications(int id) : base(p=> p.Id == id)
        {
            ApplayInclouds();

		}

		public ProductSpecifications(ProductSpecParams ProductSpec) : base(
			p => (string.IsNullOrEmpty(ProductSpec.Search) || (p.Name.ToLower().Contains(ProductSpec.Search)) &&
			(!ProductSpec.BrandId.HasValue || ProductSpec.BrandId == p.BrandId) &&
			(ProductSpec.TypeId.HasValue || ProductSpec.TypeId == p.TypeId)
			))
        {
			if (!string.IsNullOrEmpty(ProductSpec.Sort))
			{
				switch(ProductSpec.Sort)
				{
					case "PriceAsc": AddOrderBy(p => p.Price);
						break;
					case "PriceDecs": AddOrdrByDesc(p => p.Price);
						break;
					default: AddOrderBy(p => p.Name);
						break;
				}
			}
			else
			{
				AddOrderBy(p => p.Name);
			}
			ApplayInclouds();

			//number of product
			//page size
			//page index

			ApplyPagination(ProductSpec.PageSize * (ProductSpec.pageindex - 1) , ProductSpec.PageSize);
		}
        private void ApplayInclouds()
        {
			Incloud.Add(P => P.Brand);
			Incloud.Add(P => P.Type);
		}
    }
}
