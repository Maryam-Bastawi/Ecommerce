using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store4.PIs.Errors;

namespace Store4.PIs.Controllers
{
	[Route("error/{code}")]
	[ApiController]
	[ApiExplorerSettings(IgnoreApi =true)]
	public class ErrorsController : ControllerBase
	{
		//endpoint
		public IActionResult Error(int code)
		{
			return NotFound(new ApiErrorResponse (StatusCodes.Status404NotFound ,"NOT FOUND ENDPOINT"));
		}
	}
}
