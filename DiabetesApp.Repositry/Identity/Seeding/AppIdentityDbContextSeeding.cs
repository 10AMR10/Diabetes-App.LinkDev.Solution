using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Identity.Seeding
{
	public static class AppIdentityDbContextSeeding
	{
		public static async Task SeedingIdentityAsync(UserManager<IdentityUser> userManager)
		{
			if (userManager.Users.Count()==0)
			{
				var user = new IdentityUser()
				{
					Email = "Admin@gmail.com",
					UserName = "Admin"
				};
				await userManager.CreateAsync(user, "Admin123"); 
			}
		}
	}
}
