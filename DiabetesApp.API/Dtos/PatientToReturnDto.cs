using DiabetesApp.Core.Enitities;

namespace DiabetesApp.API.Dtos
{
	public class PatientToReturnDto
	{
        public int Id { get; set; }
		public string Name { get; set; }
		public ICollection<PhysiologicalIndicatorDto> PhysiologicalIndicatorsList { get; set; } = new HashSet<PhysiologicalIndicatorDto>();
        public string Hospital { get; set; }
    }
}
