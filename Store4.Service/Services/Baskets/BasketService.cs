using AutoMapper;
using Store4.Core.Dtos.Basket;
using Store4.Core.Entities;
using Store4.Core.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Service.Services.Baskets
{
	public class BasketService : IBasketService
	{
		private readonly IBasketRepository _basketRepository;
		private readonly IMapper _mapper;

		public BasketService(IBasketRepository basketRepository, IMapper mapper)
        {
			_basketRepository = basketRepository;
			_mapper = mapper;
		}
		public async Task<CoustomerBasketDto?>GetBasketAsync(string basketid)
		{
			var basket =await _basketRepository.GetBasketAsync(basketid);
			if (basket is null) return _mapper.Map<CoustomerBasketDto>(new CoustomerBasket() { id = basketid });
			return _mapper.Map<CoustomerBasketDto>(basket);
		}
		public async Task<CoustomerBasketDto?>UpDateBasketAsync(CoustomerBasketDto BasketDto)
		{
			var basket = await _basketRepository.UpDateBasketAsync(_mapper.Map<CoustomerBasket>(BasketDto));
			if (basket is null) return null;
			return _mapper.Map<CoustomerBasketDto>(basket);
		}
		public async Task<bool> DeleteBasketAsync(string basketid)
		{
			return await _basketRepository.DeleteBasketAsync(basketid);
		}

		

		
	}
}
