using DiabetesApp.Core.Enitities;
using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class PatientDto
	{
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string Gender { get; set; }
		[Required]
		[Phone]
		public string PhoneNumber { get; set; }
		[Required]
		public int BirthCount { get; set; }
		[Required]

		public bool IsPregnant { get; set; }
		[Required]
		public string Address { get; set; }
		[Required]
		public string? Code { get; set; }
		[Required]
		public int HospitalId { get; set; }
	}
}
