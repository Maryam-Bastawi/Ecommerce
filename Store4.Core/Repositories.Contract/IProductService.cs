using Store4.Core.Dtos.Products;
using Store4.Core.Helper;
using Store4.Core.Mapping.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Repositories.Contract
{
	public interface IProductService
	{
		Task<PaginationResponse<ProductDto>> GetAllProducts(ProductSpecParams ProductSpec);
		Task<IEnumerable<TypeBrandDto>> GetAllTypes();
		Task<IEnumerable<TypeBrandDto>> GetAllBrands();
		Task<ProductDto> GetProductById(int id);

	}
}
