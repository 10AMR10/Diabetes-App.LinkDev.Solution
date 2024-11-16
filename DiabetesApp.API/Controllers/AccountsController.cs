using DiabetesApp.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountsController(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager)
        {
			this._userManager = userManager;
			this._signInManager = signInManager;
		}
        // Make Login (Login Dto)
        [HttpGet("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto input)
		{
			var user=await _userManager.FindByEmailAsync(input.Email);
			if (user is null)
				return BadRequest(new ApiResponse(400, "Email Not Exist"));
			var res = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
			if (!res.Succeeded)
				return BadRequest(new ApiResponse(400, "Wrong Password"));
			return Ok(new UserDto
			{
				Email=input.Email,
				Password=input.Password,
				UserName=user.UserName
			});
		}
    }
}
