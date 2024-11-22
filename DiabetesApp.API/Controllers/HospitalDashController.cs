using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Enitities.Identity;
using DiabetesApp.Core.Repositry.contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	
	[Route("api/[controller]")]
	[ApiController]
	public class HospitalDashController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<ApplicationUser> _userManager;

		public HospitalDashController(IMapper mapper,IUnitOfWork unitOfWork,UserManager<ApplicationUser> userManager )
        {
			this._mapper = mapper;
			this._unitOfWork = unitOfWork;
			this._userManager = userManager;
		}
		// create hospital (hospitalDto) => bool
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<ActionResult<bool>> CreateHospital([FromBody]HospitalDto input)
		{
			var hospital = _mapper.Map<Hospitail>(input);
			await _unitOfWork.GetRepo<Hospitail>().AddAsync(hospital);
			var res = await _unitOfWork.CompeleteAsync();
			if(res>0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// update hospital (hospitalUpdatedDto) => 
		[Authorize(Roles = "Admin")]
		[HttpPut("{id}")]
		public async Task<ActionResult<bool>> UpdateHospital(int id,[FromBody] HospitalDto input)
		{
			var hospital = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(id);

			if (hospital is null)
				return NotFound(new ApiResponse(404));

			_mapper.Map(input, hospital);
			 _unitOfWork.GetRepo<Hospitail>().Update(hospital);
			var res= await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// Delete hospital (HospitalUpdatedDto input)
		[Authorize(Roles = "Admin")]
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeleteHospital(int id)
		{
			var hospital = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(id);

			if (hospital is null)
				return NotFound(new ApiResponse(404));

			
			_unitOfWork.GetRepo<Hospitail>().Delete(hospital);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		[Authorize(Roles = "Admin")]
		[HttpGet("{id}")]
		public async Task<ActionResult<Hospitail>?> GetHospitail(int id)
		{
			var hospital=await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(id);
			var mapped=_mapper.Map<HospitalToReturnDto>(hospital);

			if (hospital is null)
				return NotFound(new ApiResponse(404));
			return Ok(mapped);
		}
		[Authorize(Roles = "Admin, Employee")]
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<Hospitail>>> GetHospitails()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			var hospitals = await _unitOfWork.GetRepo<Hospitail>().GetAllAsync();
			if (user.HospitalId is not null)
				hospitals = hospitals.Where(x => x.Id == user.HospitalId);
			if (hospitals is null)
				return NotFound(new ApiResponse(404));
			var mapped=_mapper.Map<IEnumerable<HospitalToReturnDto>>(hospitals);
			return Ok(mapped);
		}



	}
}
