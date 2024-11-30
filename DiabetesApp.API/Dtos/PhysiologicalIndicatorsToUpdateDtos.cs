using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorsToUpdateDtos
	{
		 
		public string Date { get; set; }
		 
		public string Time { get; set; }

		public double? HealthStatusScore { get; set; }


		public string? HealthStatus { get; set; }
		 
		public double GlucoseLevel { get; set; }
		 
		public double BloodPressure { get; set; }
		 
		public double Temperature { get; set; }
	}
}
