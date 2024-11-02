using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PatientsController : ControllerBase
	{
		private readonly IGenericRepositry<Patient> _patientRepo;

		public PatientsController(IGenericRepositry<Patient> PatientRepo)
		{
			_patientRepo = PatientRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<string>>> GetAllPatients()
		{
			var patients = await _patientRepo.GetAllAsync();

			if (patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });

			var patientsNames = patients.Select(x => x.Name).ToList();

			return Ok(patientsNames);
		}

		[HttpGet("{name}")]
		public async Task< ActionResult< IEnumerable<PhysiologicalIndicatorDto>>> GetPatient(string name)
		{
			var Patients=await _patientRepo.GetAsync(name);
			if (Patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });
			var physiologicalIndicators = Patients.PhysiologicalIndicatorsList.Select(x => new PhysiologicalIndicatorDto
			{
				Time = x.Time,
				Date = x.Date,
				GlucoseLevel = x.GlucoseLevel,

			});
			return Ok(physiologicalIndicators);
		}

	}
}
