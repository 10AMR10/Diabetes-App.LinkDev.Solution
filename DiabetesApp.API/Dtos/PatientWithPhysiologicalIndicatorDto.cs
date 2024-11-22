namespace DiabetesApp.API.Dtos
{
	public class PatientWithPhysiologicalIndicatorDto
	{
        public int id{ get; set; }
        public string name { get; set; }
		public string code { get; set; }
        public string  HealthStatusInThisDate { get; set; }
    }
}
