﻿using AutoMapper;
using DiabetesApp.API.Dtos;
using DiabetesApp.API.External;
using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Core.specificaitons.patients;
using DiabetesApp.Core.specificaitons.physiologicalIndicators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Text.Json;
using Talabat.APIs.Errors;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace DiabetesApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PhysiologicalIndicatorsController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;
		private readonly HttpClient _client;


		public PhysiologicalIndicatorsController(IMapper mapper, IUnitOfWork unitOfWork, HttpClient client)
		{
			this._mapper = mapper;
			this._unitOfWork = unitOfWork;
			this._client = client;
		}
		// create PhysiologicalIndicators (PhysiologicalIndicatorsDto) => bool
		[HttpPost]
		public async Task<ActionResult<bool>> CreatePhysiologicalIndicators([FromBody] PhysiologicalIndicatorsDto input)
		{
			var patient = await _unitOfWork.GetRepo<Patient>().GetByIdAsync(input.PatientId);
			if (patient is null)
				return BadRequest(new ApiResponse(400));
			try
			{
				var content = new StringContent(JsonSerializer.Serialize(new { sugarPercentage = input.GlucoseLevel, bloodPressure = input.BloodPressure, averageTemprature = input.Temperature }), System.Text.Encoding.UTF8, "application/json");
				var response = await _client.PostAsync("https://api-model-kohl.vercel.app/predict", content);
				if (response.IsSuccessStatusCode)
				{
					var responseContent = await response.Content.ReadAsStringAsync();
					var data = JsonSerializer.Deserialize<ExternalAPIResponse>(responseContent);
					input.HealthStatusScore = (int)data.predictedHealthStatusScore;
				}
				else
				{
					return StatusCode((int)response.StatusCode, response.ReasonPhrase);
				}
			}
			catch (Exception ex)
			{
				return StatusCode(500, $"Internal server error: {ex.Message}");
			}


			if (input.BloodPressure > 140 || input.GlucoseLevel > 125) input.HealthStatus = "Dangerous";
			else if (input.HealthStatusScore >= 80) input.HealthStatus = "Good";
			else if (input.HealthStatusScore >= 60) input.HealthStatus = "Stable";
			else input.HealthStatus = "Unhealthy";





			var physiologicalIndicators = new PhysiologicalIndicators()
			{
				Date = DateOnly.Parse(input.Date),
				Time = TimeOnly.Parse(input.Time),
				BloodPressure = input.BloodPressure,
				GlucoseLevel = input.GlucoseLevel,
				HealthStatusScore = input.HealthStatusScore,
				HealthStatus = input.HealthStatus,
				Temperature = input.Temperature,
				PatientId = input.PatientId,
			};
			physiologicalIndicators.Patient = patient;
			physiologicalIndicators.Patient.LatestHealthStatus = physiologicalIndicators.HealthStatus;
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

			physiologicalIndicators.Date = DateOnly.Parse(input.Date);
			physiologicalIndicators.Time = TimeOnly.Parse(input.Time);
			physiologicalIndicators.BloodPressure = input.BloodPressure;
			physiologicalIndicators.GlucoseLevel = input.GlucoseLevel;
			physiologicalIndicators.HealthStatus = input.HealthStatus;
			physiologicalIndicators.Temperature = input.Temperature;
			physiologicalIndicators.PatientId = input.PatientId;
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

		// get by (name ,date1,date2)
		[HttpGet("{name}/{date1}/{date2}")]
		public async Task<ActionResult<IReadOnlyList<PhysiologicalIndicatorToRetunrDto>>> GetByDate(string name, DateOnly date1, DateOnly date2)
		{
			var spec = new PatientWithAllDataSpec(name);
			var patient = await _unitOfWork.GetRepo<Patient>().GetSpecAsync(spec);
			if (patient is null)
				return BadRequest(new ApiResponse(400));
			//var patientId = patient.Id;
			//var spec

			//var physiologicalIndicator = await _unitOfWork.GetRepo<PhysiologicalIndicators>().GetSpecAsync
			var phy = patient.PhysiologicalIndicatorsList?.Where(x => x.Date > date1 && x.Date < date2);
			if (phy is null)
				return BadRequest(new ApiResponse(400));

			var mapped = _mapper.Map<IEnumerable<PhysiologicalIndicatorToRetunrDto>>(phy);
			return Ok(mapped);

		}
		// get allPhysiologicalIndicator() =>IReadOnlyList<PhysiologicalIndicatorAndDateDto>
		[HttpGet("All")]
		public async Task<ActionResult<IReadOnlyList<PhysiologicalIndicatorAndDateDto>>> GetAllDates()
		{
			var spec = new PhysiologicalIndicatorsSpecification();
			var physios = await _unitOfWork.GetRepo<PhysiologicalIndicators>().GetAllSpecAsync(spec);


			var mapped = physios.OrderBy(o => o.Date)
				.GroupBy(g => g.Date)
				.Select(x => new PhysiologicalIndicatorAndDateDto
				{
					count = physios.Count(x => x.HealthStatus == "Dangerous"),
					Date = x.Key,

				});


			return Ok(mapped);
		}








	}
}
