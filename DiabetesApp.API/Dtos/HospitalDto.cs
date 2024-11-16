using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class HospitalDto
	{
		[Required]
		public string HospitalName { get; set; }
		[Required]
		public string Location { get; set; }
		[Required]
		public string Country { get; set; }
		[Required]
		public string City { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string Province { get; set; }
	}
}
