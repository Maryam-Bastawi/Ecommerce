using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store4.Core.Entities.Identity;
using Store4.PIs.Errors;
using System.Security.Claims;

namespace Store4.PIs.Extensions
{
	public static class UserMangerExtensions
	{
		public static async Task<AppUser> FindByEmailWithAddressAsync(this UserManager<AppUser> userManager,ClaimsPrincipal User)
		{
			var userEmail = User.FindFirstValue(ClaimTypes.Email);
			if (userEmail is null) return null;

		  var user = await	userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == userEmail);
			if (user is null) return null;

			return user;
		}
	}
}
