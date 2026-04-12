using DiabetesApp.Core.Enitities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DiabetesApp.Repositry.Identity
{
	public class AppIdentityDbContext : IdentityDbContext<ApplicationUser>
	{
		//4
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
		{

		}
	}
}
