using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Core.specificaitons;
using DiabetesApp.Core.specificaitons.patients;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PatientsController : ControllerBase
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;

		public PatientsController(IUnitOfWork unitOfWork,IMapper mapper)
		{
			
			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<string>>> GetAllPatients()
		{
			
			var patients = await _unitOfWork.GetRepo<Patient>().GetAllAsync();

			if (patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });

			var patientsNames = patients.Select(x => x.Name).ToList();
			//var mapped= _mapper.Map<IEnumerable<Patient>,IEnumerable<PatientToReturnDto>>(patients);

			return Ok(patientsNames);
		}

		[HttpGet("{name}")]
		public async Task< ActionResult< IEnumerable<PhysiologicalIndicatorToRetunrDto>>> GetPatient(string name)
		{
			var spec = new PatientWithAllDataSpec(name);
			var Patients = await _unitOfWork.GetRepo<Patient>().GetSpecAsync(spec);
			if (Patients is null)
				return NotFound(new ApiResponse(404));
			var mapped = _mapper.Map<IEnumerable<PhysiologicalIndicatorToRetunrDto>>(Patients.PhysiologicalIndicatorsList);
			//var physiologicalIndicators = Patients.PhysiologicalIndicatorsList.Select(x => new PhysiologicalIndicatorDto
			//{
			//	Time = x.Time,
			//	Date = x.Date,
			//	GlucoseLevel = x.GlucoseLevel,

			//});
			return Ok(mapped);
		}
		// create Patient (PatientDto) => bool
		[HttpPost]
		public async Task<ActionResult<bool>> CreatePatient([FromBody] PatientDto input)
		{
			var patient = _mapper.Map<Patient>(input);
			patient.Hospital=await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(input.HospitalId);
			await _unitOfWork.GetRepo<Patient>().AddAsync(patient);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// update patient (patientID ,PatientDto) => bool
		[HttpPut("{id}")]
		public async Task<ActionResult<bool>> UpdatePatiet(int id, [FromBody] PatientDto input)
		{
			var patient = await _unitOfWork.GetRepo<Patient>().GetByIdAsync(id);

			if (patient is null)
				return NotFound(new ApiResponse(404));

			_mapper.Map(input, patient);
			patient.Hospital = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(input.HospitalId);

			_unitOfWork.GetRepo<Patient>().Update(patient);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
		// Delete Pateint (int id)
		[HttpDelete("{id}")]
		public async Task<ActionResult<bool>> DeletePatient(int id)
		{
			var patient = await _unitOfWork.GetRepo<Patient>().GetByIdAsync(id);

			if (patient is null)
				return NotFound(new ApiResponse(404));


			_unitOfWork.GetRepo<Patient>().Delete(patient);
			var res = await _unitOfWork.CompeleteAsync();
			if (res > 0)
				return Ok(true);
			return BadRequest(new ApiResponse(400));

		}
	}
}
