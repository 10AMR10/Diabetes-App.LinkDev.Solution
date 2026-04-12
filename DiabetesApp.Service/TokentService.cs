using DiabetesApp.Core.Enitities.Identity;
using DiabetesApp.Core.Service.Contract;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Service
{
	public class TokentService : ITokentService
	{
		private readonly IConfiguration configuration;

		public TokentService(IConfiguration configuration)
		{
			this.configuration = configuration;
		}
		public async Task<string> CreateTokenAsync(ApplicationUser user, UserManager<ApplicationUser> userManager)
		{
			//Payload
			//private claims => user...
			var authClaims = new List<Claim>()
			{
				new Claim(ClaimTypes.Email,user.Email),
				
			};
			// claims to roles
			var roles = await userManager.GetRolesAsync(user);
			foreach (var role in roles)
				authClaims.Add(new Claim(ClaimTypes.Role, role));
			//key
			var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]));
			// anotherclaims
			var Token = new JwtSecurityToken(
				issuer: configuration["JWT:ValidIssuer"],
				audience: configuration["JWT:validAudience"],
				expires: DateTime.Now.AddDays(double.Parse(configuration["JWT:DurationInDays"])),
				claims: authClaims,
				signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256Signature)

				);
			return new JwtSecurityTokenHandler().WriteToken(Token);

		}
	}
}
