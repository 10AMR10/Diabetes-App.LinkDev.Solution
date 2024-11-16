namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorToRetunrDto
	{
		public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
		public double GlucoseLevel { get; set; }
	}
}
