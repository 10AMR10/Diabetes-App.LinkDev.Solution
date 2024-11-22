using DiabetesApp.Core.Enitities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Service.Contract
{
	
		public interface ITokentService
		{
			Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager);
		}
	
}
