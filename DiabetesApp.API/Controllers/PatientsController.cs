using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Enitities.Identity;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Core.specificaitons;
using DiabetesApp.Core.specificaitons.patients;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Claims;
using Talabat.APIs.Errors;

namespace DiabetesApp.API.Controllers
{
	[Authorize(Roles = "Admin, Employee")]
	[Route("api/[controller]")]
	[ApiController]
	public class PatientsController : ControllerBase
	{

		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;

		public PatientsController(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
		{

			this._unitOfWork = unitOfWork;
			this._mapper = mapper;
			this._userManager = userManager;
		}
		[HttpGet("All")]
		public async Task<ActionResult<IEnumerable<string>>> GetAllPatients()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			var patients = await _unitOfWork.GetRepo<Patient>().GetAllAsync();
			if (patients is null)
				return NotFound(new ApiResponse(404));
			if (user.HospitalId is not null)
				patients = patients.Where(x => x.HospitalId == user.HospitalId);

			if (patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });

			var patientsName= patients.Select(x => new {Id=x.Id, Name = x.Name, LatestHealthStatus = x.LatestHealthStatus }).ToList();
			//var mapped= _mapper.Map<IEnumerable<Patient>,IEnumerable<PatientToReturnDto>>(patients);

			return Ok(patientsName);
		}
		[HttpGet("AllDetails")]
		public async Task<ActionResult<IReadOnlyList<PatientToReturnDto>>> GetAllPatientsDetails()
		{
			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			var spec = new PatientWithAllDataSpec();
			var patients = await _unitOfWork.GetRepo<Patient>().GetAllSpecAsync(spec);
			if (patients is null)
				return NotFound(new ApiResponse(404));
			if (user.HospitalId is not null)
				patients = patients.Where(x => x.HospitalId == user.HospitalId);

			if (patients is null)
				return NotFound(new { Message = "Patient Not Found", StatusCode = 400 });

			var mapped= _mapper.Map<IReadOnlyList<PatientToReturnDto>>(patients);
			
			//var mapped= _mapper.Map<IEnumerable<Patient>,IEnumerable<PatientToReturnDto>>(patients);

			return Ok(mapped);
		}

		[HttpGet("ByName/{name}")]
		public async Task<ActionResult<IEnumerable<PhysiologicalIndicatorToRetunrDto>>> GetPatient(string name)
		{


			var email = User.FindFirstValue(ClaimTypes.Email);
			var user = await _userManager.FindByEmailAsync(email);
			var spec = new PatientWithAllDataSpec();
			var patients = await _unitOfWork.GetRepo<Patient>().GetAllSpecAsync(spec);

			if (user.HospitalId is not null)
				patients = patients?.Where(x => x.HospitalId == user.HospitalId);
			if (patients is null)
				return NotFound(new ApiResponse(404, "There Is no Patients In Your Hospital"));
			var patient = patients.FirstOrDefault(x => x.Name == name);
			if (patient is null)
				return NotFound(new ApiResponse(404, "Patient Is Not Exist"));
			var mapped = _mapper.Map<IEnumerable<PhysiologicalIndicatorToRetunrDto>>(patient.PhysiologicalIndicatorsList);
			//var physiologicalIndicators = Patients.PhysiologicalIndicatorsList.Select(x => new PhysiologicalIndicatorDto
			//{
			//	Time = x.Time,
			//	Date = x.Date,
			//	GlucoseLevel = x.GlucoseLevel,

			//});
			return Ok(mapped);
		}

		[HttpGet("ById/{id}")]
		public async Task<ActionResult<PatientDto>> GetPatientById(int id)
		{
			var patients= await _unitOfWork.GetRepo<Patient>().GetByIdAsync(id);
			if (patients is null)
				return NotFound(new ApiResponse(400));
			var mapped=_mapper.Map<PatientDto>(patients);
			return Ok(mapped);
		}
		// create Patient (PatientDto) => bool

		[HttpPost]
		public async Task<ActionResult<bool>> CreatePatient([FromBody] PatientDto input)
		{
			
			var patient = _mapper.Map<Patient>(input);
			patient.Hospital = await _unitOfWork.GetRepo<Hospitail>().GetByIdAsync(input.HospitalId);

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
