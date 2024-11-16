using DiabetesApp.Core.Enitities;
using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorsDto
	{
		[Required]
		public DateOnly Date { get; set; }
		[Required]
		public TimeOnly Time { get; set; }
		[Required]
		public string HealthStatus { get; set; }
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
