using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using store4.Repository.Data.Contexts;
using Store4.PIs.Errors;

namespace Store4.PIs.Controllers
{
	public class BuggyController : BaseApiController
	{
		private readonly StoreDbContext _context;

		public BuggyController(StoreDbContext context)
		{
			_context = context;
		}

		[HttpGet("notfound")]//pathurl/api/buggy/notfound
		public async Task<IActionResult> GetNotFoundRequest()
		{
		  var brand= await	_context.ProductBrands.FindAsync(100);
			if (brand is null) return NotFound(new ApiErrorResponse(404 , "brand with id ; 100 is not found") );
			return Ok(brand);
		}
		[HttpGet("ServerError")]//pathurl/api/buggy/ServerError
		public async Task<IActionResult> GetServerError()
		{
		  var brand= await	_context.ProductBrands.FindAsync(100);
			var brandtostring = brand.ToString();//will throw exption(null refrence exption) 
			return Ok(brandtostring);
		}

		[HttpGet("BadRequest")]//pathurl/api/buggy/BadRequest
		public async Task<IActionResult> GetBadRequest()
		{
			
			return BadRequest(new ApiErrorResponse(400));
		}
		[HttpGet("BadRequest/{id}")]//pathurl/api/buggy/BadRequest/ahmed
		public async Task<IActionResult> GetBadRequest(int id)//validation error
		{
			
			return Ok();
		}
		[HttpGet("Unauthorized/{id}")]//pathurl/api/buggy/Unauthorized
		public async Task<IActionResult> GetUnauthorized(int id)//validation error
		{
			
			return Unauthorized(new ApiErrorResponse(400));
		}


	}
}
