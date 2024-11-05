using Microsoft.AspNetCore.Identity;
using Store4.Core.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Repository.Identity
{
	public static class StoreIdentityDbContextSeed
	{
		public async static Task SeedAppUserAsync(UserManager<AppUser> _userManager)
		{

			if(_userManager.Users.Count() == 0) {
				var user = new AppUser()
				{
					Email = "MaryamBastawi1@gmail.com",
					DisplayName = "maryam bastawi",
					UserName = "maryam.bastawi",
					PhoneNumber = "01017073523",
					Address = new Address()
					{
						FName = "maryam",
						LName = "bastawi",
						City = "ciaro",
						country = "egypt",
						Street = "talal",
					}
				};
				await _userManager.CreateAsync(user, "P@ssW0rd");
			}
			
		}
	}
}