using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Store4.Core.Entities.Identity;
using Store4.Core.Services.Contract;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Service.Services.Tokens
{
	public class TokenService : ITokenService
	{
		private readonly IConfiguration _configuration;

		public TokenService(IConfiguration configuration  )
        {
			_configuration = configuration;
		}
        public async Task<string> CreateTokenAsync(AppUser user, UserManager<AppUser> userManager)
		{
			var authclaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.Email,user.Email),
				new Claim(ClaimTypes.MobilePhone,user.PhoneNumber),
				
			};
			var userRoles = await userManager.GetRolesAsync(user);
            foreach (var role in userRoles)
            {
				authclaims.Add(new Claim(ClaimTypes.Role, role));
            }
			var authkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
			//1-headr(algo-type)
			//2-paylaod(claims)
			//3-signature
			var token = new JwtSecurityToken(
				issuer: _configuration["Jwt:Issuer"],
				audience: _configuration["Jwt:Audience"],
				expires: DateTime.Now.AddDays(double.Parse(_configuration["Jwt:DurationInDays"])),
				claims: authclaims,
				signingCredentials: new SigningCredentials(authkey,SecurityAlgorithms.HmacSha256Signature)

				);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
