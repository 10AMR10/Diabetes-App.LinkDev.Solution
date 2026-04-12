using DiabetesApp.Core.Enitities;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace DiabetesApp.API.Dtos
{
	public class PatientToReturnDto
	{
		public int Id { get; set; }
		 
		public string Name { get; set; }
		 
		public int Age { get; set; }
		 
		public string Gender { get; set; }
		 
		
		public string PhoneNumber { get; set; }
		 
		public int BirthCount { get; set; }
		 

		public bool IsPregnant { get; set; }
		 
		public string Address { get; set; }
		 
		public string? Code { get; set; }
		 
		public int HospitalId { get; set; }
        public string HospitalName { get; set; }
    }
}
