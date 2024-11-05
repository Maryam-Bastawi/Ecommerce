using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Store4.Core.Dtos.Products;
using Store4.Core.Helper;
using Store4.Core.Mapping.Products;
using Store4.Core.Repositories.Contract;
using Store4.PIs.Errors;

namespace Store4.PIs.Controllers
{
	public class ProductController : BaseApiController
	{
		private readonly IProductService _productService;

		public ProductController(IProductService productService)
        {
			_productService = productService;
		}

		[ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] //baseurl / api / product
		[Authorize]
		//sort:name/price asc /price desc
		public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams ProductSpec)//endpoint
		{
		 var result = await	_productService.GetAllProducts(ProductSpec);
			//return Ok(new PaginationResponse<ProductDto>(ProductSpec.PageSize, ProductSpec.pageindex, 0,result));
			return Ok(result);
		}
		[ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

		[HttpGet("types")] //baseurl / api / product/types
		[Authorize]
		public async Task<ActionResult<IEnumerable<TypeBrandDto>>> Getalltypes()//endpoint
		{
			var result = await _productService.GetAllTypes();
			return Ok(result);
		}
		[ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]

		[HttpGet("Brands")] //baseurl / api / product/Brands
		[Authorize]
		public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetallBrans()//endpoint
		{
			var result = await _productService.GetAllBrands();
			return Ok(result);
		}

		[ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]

		[HttpGet("id")] //baseurl / api / product/id
		public async Task<ActionResult<ProductDto>> GetProductsById(int? id)//endpoint
		{
			if (id is null) return BadRequest(new ApiErrorResponse(400));
		
		    var result = await	_productService.GetProductById(id.Value);
			if (result is null) return NotFound(new ApiErrorResponse(404));
			
			return Ok(result);
		}



	}
}
