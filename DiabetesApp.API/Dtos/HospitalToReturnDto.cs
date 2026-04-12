using DiabetesApp.Core.Enitities.Identity;
using DiabetesApp.Core.Enitities;

namespace DiabetesApp.API.Dtos
{
	public class HospitalToReturnDto
	{
		public string HospitalName { get; set; }
		public string Location { get; set; }
		public string Country { get; set; }
		public string City { get; set; }
		public string Address { get; set; }
		public string Province { get; set; }
        public int Id { get; set; }
    }
}
