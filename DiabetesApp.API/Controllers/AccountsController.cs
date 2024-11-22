using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Enitities.Identity;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Core.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		
		private readonly ITokentService _tokentService;
		private readonly IUnitOfWork _unitOfWork;

		public AccountsController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager
			,  ITokentService tokentService
			, IUnitOfWork unitOfWork)
		{
			this._userManager = userManager;
			this._signInManager = signInManager;
			
			this._tokentService = tokentService;
			this._unitOfWork = unitOfWork;
		}
		// Make Login (Login Dto)
		[HttpGet("Login")]
		public async Task<ActionResult<UserDto>> Login(LoginDto input)
			{
			var user = await _userManager.FindByEmailAsync(input.Email);

			if (user is null)
				return BadRequest(new ApiResponse(400, "Email Not Exist"));
			var res = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
			if (!res.Succeeded)
				return BadRequest(new ApiResponse(400, "Wrong Password"));
			return Ok(new UserDto
			{
				Email = input.Email,
				
				UserName = user.UserName,
				HospitalId = user.HospitalId,
				Token = await _tokentService.CreateTokenAsync(user, _userManager)
			});
		}
		// create user  (RegisterDto) => UserDto
		[Authorize(Roles ="Admin")]
		[HttpPost]
		public async Task<ActionResult<UserDto>> CreateUser(RegisterDto input)
		{
			if (await _userManager.FindByEmailAsync(input.Email) is not null)
				return BadRequest(new ApiResponse(400, "Dublicated Email"));
			var user = new ApplicationUser
			{
				Email = input.Email,
				HospitalId = input.HospitalId,
				UserName = input.UserName,
			};
			var res = await _userManager.CreateAsync(user, input.Password);
			if (!res.Succeeded)
				return BadRequest(new ApiResponse(400));

			await _userManager.AddToRoleAsync(user, "Employee");
			return Ok(new UserDto
			{
				Email = input.Email,
				
				UserName = user.UserName,
				HospitalId = user.HospitalId,
				Token = await _tokentService.CreateTokenAsync(user, _userManager)
			});
		}
		// get all users () => list of usersDto
		[Authorize(Roles = "Admin")]
		[HttpGet("All")]
		public async Task<ActionResult<IReadOnlyList<UserDto>>> GetAllUsers()
		{
			var users = await _userManager.Users.ToListAsync();
			List<ApplicationUser> EmpUsers = new List<ApplicationUser>();
			foreach (var user in users)
			{
				var role = await _userManager.GetRolesAsync(user);
				if (role.Contains("Employee"))
					EmpUsers.Add(user);
			}
			var mapped = await Task.WhenAll(EmpUsers.Select(async x =>
			{
				var hos = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(x.HospitalId);
				return new UserToReturnDto
				{
					Email = x.Email,
					UserName = x.UserName,
					HospitalName = hos?.HospitalName
				};
			}));
			return Ok(mapped);
		}
		[Authorize(Roles = "Admin")]
		[HttpDelete("{email}")]
		public async Task<ActionResult<bool>> DeleteUser(string email)
		{
			var user = await _userManager.FindByIdAsync(email);
			if (user is null)
				return BadRequest(new ApiResponse(400));
			var res = await _userManager.DeleteAsync(user);
			if (!res.Succeeded)
				return BadRequest(new ApiResponse(400));
			return Ok(true);
		}
		[Authorize(Roles = "Admin, Employee")]
		[HttpGet("CurrentUser")]
		public async Task<ActionResult<UserDto>> GetCurrentUser()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			var hos = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(user.HospitalId);
			return Ok(new UserToReturnDto
			{
				Email = user.Email,
				UserName = user.UserName,
				HospitalName = hos is null ? "" : hos.HospitalName
			});
		}


	}
}
