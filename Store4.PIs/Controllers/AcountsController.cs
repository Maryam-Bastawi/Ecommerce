using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore .Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Store4.Core.Dtos.Auth;
using Store4.Core.Entities.Identity;
using Store4.Core.Services.Contract;
using Store4.PIs.Errors;
using Store4.PIs.Extensions;
using Store4.Service.Services.Tokens;
using System.Security.Claims;

namespace Store4.PIs.Controllers
{
	
	public class AcountsController : BaseApiController
	{
		private readonly IUserService _userService;
		private readonly UserManager<AppUser> _userManager;
		private readonly ITokenService _tokenService;
		private readonly IMapper _mapper;

		public AcountsController(IUserService userService
			, UserManager<AppUser> userManager,
			ITokenService tokenService,
		     
			IMapper mapper)
        {
			_userService = userService;
			_userManager = userManager;
			_tokenService = tokenService;
			_mapper = mapper;
		}
		[HttpPost("login")]
        public async Task<ActionResult<UserDto>> LoginIn(LoginDto loginDto)
		{
		  var user = await	_userService.LoginAsync(loginDto);
			if (user is null) return Unauthorized(new ApiErrorResponse(StatusCodes.Status401Unauthorized));
			return Ok(user);
		}
		[HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto RregisterDto)
		{
		  var user = await	_userService.RegisterAsync(RregisterDto);
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			return Ok(user);
		}

		
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			var user = await _userManager.FindByEmailAsync(userEmail);
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return Ok(new UserDto()
			{
				Email = user.Email,
				DisplayName = user.DisplayName,
				Token = await _tokenService.CreateTokenAsync(user, _userManager)
			});
		}
		
		[HttpGet("Address")]
		public async Task<ActionResult<UserDto>> GetCurrentUserAddress()
		{
			var userEmail =  User.FindFirstValue(ClaimTypes.Email);
			if (userEmail is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));
			var user = await _userManager.FindByEmailWithAddressAsync(User);
			if (user is null) return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest));

			return base.Ok(_mapper.Map<Core.Dtos.Auth.AddressDto>(user.Address));
		}

		[HttpGet("Update")]
		public async Task<ActionResult<UserDto>> UpdateCurrentUserAddress(string id)
		{
			var getid = await _userManager.FindByIdAsync(id);
			if (getid is null) BadRequest(new ApiErrorResponse(StatusCodes.Status404NotFound, "id not found"));
		   
			return Ok(getid);
		}

		[HttpPost("Update")]
		public async Task<ActionResult<UserDto>> UpdateCurrentUserAddress(Core.Dtos.Auth.AddressDto addressDto)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (string.IsNullOrEmpty(userEmail))
			{
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Email not found"));
			}
			var user = await _userManager.FindByEmailWithAddressAsync(User);

			if (user is null)
			{return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "User not found."));}
	       var result = new Core.Dtos.Auth.AddressDto()
			{   FName = user.Address.FName,
				LName = user.Address.LName,
				country = user.Address.country,
				Street = user.Address.Street,  
				City = user.Address.City,};

			var resultupdate = await _userManager.UpdateAsync(user);
			if (!resultupdate.Succeeded)
			{
				return BadRequest(new ApiErrorResponse(StatusCodes.Status400BadRequest, "Failed to update the address."));

			}
			return Ok(resultupdate);
		}
	}
}
