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
		private readonly IGenericRepositry<Patient> _patientRepo;
		private readonly IMapper _mapper;

		public PatientsController(IGenericRepositry<Patient> PatientRepo,IMapper mapper)
		{
			_patientRepo = PatientRepo;
			this._mapper = mapper;
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<string>>> GetAllPatients()
		{
			
			var patients = await _patientRepo.GetAllAsync();

			if (patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });

			var patientsNames = patients.Select(x => x.Name).ToList();
			//var mapped= _mapper.Map<IEnumerable<Patient>,IEnumerable<PatientToReturnDto>>(patients);

			return Ok(patientsNames);
		}

		[HttpGet("{name}")]
		public async Task< ActionResult< IEnumerable<PhysiologicalIndicatorDto>>> GetPatient(string name)
		{
			var spec = new PatientWithAllDataSpec(name);
			var Patients=await _patientRepo.GetSpecAsync(spec);
			if (Patients is null)
				return NotFound(new ApiResponse(404));
			var mapped = _mapper.Map<IEnumerable<PhysiologicalIndicatorDto>>(Patients.PhysiologicalIndicatorsList);
			//var physiologicalIndicators = Patients.PhysiologicalIndicatorsList.Select(x => new PhysiologicalIndicatorDto
			//{
			//	Time = x.Time,
			//	Date = x.Date,
			//	GlucoseLevel = x.GlucoseLevel,

			//});
			return Ok(mapped);
		}

	}
}
