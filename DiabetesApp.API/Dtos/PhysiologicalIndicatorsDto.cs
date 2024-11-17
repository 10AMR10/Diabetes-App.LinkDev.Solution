using DiabetesApp.Core.Enitities;
using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorsDto
	{
		[Required]
		public string Date { get; set; }
		[Required]
		public string Time { get; set; }
		
		public double? HealthStatusScore { get; set; }
		

		public string? HealthStatus { get; set; }
		[Required]
		public double GlucoseLevel { get; set; }
		[Required]
		public double BloodPressure { get; set; }
		[Required]
		public double Temperature { get; set; }
		[Required]
		public int PatientId { get; set; }
	}
}
