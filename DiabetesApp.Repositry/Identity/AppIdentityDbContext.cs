using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Identity
{
	public class AppIdentityDbContext : IdentityDbContext<IdentityUser>
	{
		//3
		public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
		{

		}
	}
}
