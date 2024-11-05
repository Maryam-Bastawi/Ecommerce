using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities;
using Store4.Core.Repositories.Contract;
using Store4.PIs.Errors;

namespace Store4.PIs.Controllers
{
	
	public class BasketController : BaseApiController
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketController(IBasketRepository basketRepository , IMapper mapper)
        {
			_basketRepository = basketRepository;
			_mapper = mapper;
		}
		[HttpGet]
        public async Task<ActionResult<CoustomerBasket>>GetBasket(string id)
		{
			if(id is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest,"invalid id!"));
		var basket = await _basketRepository.GetBasketAsync(id);
			if (basket is null) return new CoustomerBasket() { id = id };
			return Ok(basket);
		}
		[HttpPost]
        public  async Task<ActionResult<CoustomerBasket>> CreateUpDateBasketAsync(CoustomerBasketDto model)
		{
			var basket =await _basketRepository.UpDateBasketAsync(_mapper.Map<CoustomerBasket>(model));
			if (basket is null) return BadRequest(new ApiErrorResponse(400));
			return Ok(basket);
		}

		[HttpDelete]
        public  async Task DeleteBasketAsync(string id)
		{
			 await _basketRepository.DeleteBasketAsync( id);
			
		}
	}
}
