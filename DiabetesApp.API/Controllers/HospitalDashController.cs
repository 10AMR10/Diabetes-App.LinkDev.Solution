using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HospitalDashController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public HospitalDashController(IMapper mapper,IUnitOfWork unitOfWork )
        {
			this._mapper = mapper;
			this._unitOfWork = unitOfWork;
		}
        // create hospital (hospitalDto) => bool
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
		[HttpPut("{id}")]
		public async Task<ActionResult<bool>> UpdateHospital(int id,[FromBody] HospitalDto input)
		{
			var hospital = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(id);

			if (hospital is null)
				return NotFound(new ApiResponse(404));

			hospital=_mapper.Map<Hospitail>(input);
			 _unitOfWork.GetRepo<Hospitail>().Update(hospital);
			var res= await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// Delete hospital (HospitalUpdatedDto input)
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

		[HttpGet("{id}")]
		public async Task<ActionResult<Hospitail>?> GetHospitail(int id)
		{
			var hospital=await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(id);
			
			if (hospital is null)
				return NotFound(new ApiResponse(404));
			return Ok(hospital);
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<Hospitail>>> GetHospitails()
		{
			var hospitals = await _unitOfWork.GetRepo<Hospitail>().GetAllAsync();
			if (hospitals is null)
				return NotFound(new ApiResponse(404));
			return Ok(hospitals);
		}



	}
}
