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
	public class PhysiologicalIndicatorsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public PhysiologicalIndicatorsController(IMapper mapper, IUnitOfWork unitOfWork)
		{
			this._mapper = mapper;
			this._unitOfWork = unitOfWork;
		}
		// create PhysiologicalIndicators (PhysiologicalIndicatorsDto) => bool
		[HttpPost]
		public async Task<ActionResult<bool>> CreatePhysiologicalIndicators([FromBody] PhysiologicalIndicatorsDto input)
		{
			var physiologicalIndicators = _mapper.Map<PhysiologicalIndicators>(input);
			physiologicalIndicators.Patient = await _unitOfWork.GetRepo<Patient>().GetByIdAsync(input.PatientId);
			await _unitOfWork.GetRepo<PhysiologicalIndicators>().AddAsync(physiologicalIndicators);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}




		// update PhysiologicalIndicators (PhysiologicalIndicatorsID ,PhysiologicalIndicatorsDto) => bool
		[HttpPut("{id}")]
		public async Task<ActionResult<bool>> UpdatePhysiologicalIndicators(int id, [FromBody] PhysiologicalIndicatorsDto input)
		{
			var physiologicalIndicators = await _unitOfWork.GetRepo<PhysiologicalIndicators>().GetByIdAsync(id);

			if (physiologicalIndicators is null)
				return NotFound(new ApiResponse(404));

			_mapper.Map(input, physiologicalIndicators);
			physiologicalIndicators.Patient = await _unitOfWork.GetRepo<Patient>().GetByIdAsync(input.PatientId);

			_unitOfWork.GetRepo<PhysiologicalIndicators>().Update(physiologicalIndicators);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// Delete PhysiologicalIndicators (int id)
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeletePhysiologicalIndicators(int id)
		{
			var physiologicalIndicators = await _unitOfWork.GetRepo<PhysiologicalIndicators>().GetByIdAsync(id);

			if (physiologicalIndicators is null)
				return NotFound(new ApiResponse(404));


			_unitOfWork.GetRepo<PhysiologicalIndicators>().Delete(physiologicalIndicators);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
	}
}
