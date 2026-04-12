namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorToRetunrDto
	{
        public int Id { get; set; }
        public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
		public double? HealthStatusScore { get; set; }
		public string HealthStatus { get; set; }
		public double GlucoseLevel { get; set; }
		public double BloodPressure { get; set; }
		public double Temperature { get; set; }
	}
}
