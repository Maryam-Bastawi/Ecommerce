using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store4.Core.Dtos.Orders;
using Store4.Core.Entities.Orders;
using Store4.Core.Repositories.Contract;
using Store4.Core.Services.Contract;
using Store4.PIs.Errors;
using System.Security.Claims;

namespace Store4.PIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
		private readonly IOrderService _orderService;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork unitOfWork;

		public OrdersController(IOrderService orderService , IMapper mapper, IUnitOfWork unitOfWork)
        {
			_orderService = orderService;
			_mapper = mapper;
			this.unitOfWork = unitOfWork;
		}
		
		[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreteOrder(OrderDto model)
        {
            var useremail =  User.FindFirstValue(ClaimTypes.Email);
            if (useremail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
            var address = _mapper.Map<Address>(model.ShipToAddress);
           var order = await  _orderService.CreateOrderAsync(useremail, model.BasketId, model.DeliveryMethodId, address);
            if (order is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
            return Ok(_mapper.Map<OrderToReturnDto>(order));
        }

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetOrderForSpecificUser()
		{
			var useremail = User.FindFirstValue(ClaimTypes.Email);
			if (useremail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
			var orders = await _orderService.GetOrderForSpecificUser(useremail);
			if (orders is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
			return Ok(_mapper.Map<IEnumerable<OrderToReturnDto>>(orders));
		}
		[Authorize]
		[HttpGet("{Orderid}")]
		public async Task<IActionResult> GetOrderByIdForSpecificUser(int Orderid)
		{
			var useremail = User.FindFirstValue(ClaimTypes.Email);
			if (useremail is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
			var order = await _orderService.GetOrderByIdForSpecificUser(useremail , Orderid);
			if (order is null) return NotFound(new ApiErrorResponse(StatusCodes.Status404NotFound));
			return Ok(_mapper.Map<OrderToReturnDto>(order));
		}
		
		[HttpGet("DeliveryMethods")]
		public async Task<IActionResult> GetDeliveryMethods()
		{
			var deliveryMethod = unitOfWork.Repository<DeliveryMethod, int>().GetAllAsync();
			if (deliveryMethod is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			
			return Ok(_mapper.Map<DeliveryMethodDto>(deliveryMethod));
		}
	}
}
