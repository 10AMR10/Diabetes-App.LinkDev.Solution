using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities
{
	public class Hospitail:BaseEntity
	{
		  
		public string HospitalName { get; set; }
		public string Location { get; set; }
		public string Country { get; set; } 
		public string City { get; set; } 
		public string Address { get; set; }
		public string Province { get; set; }

		public ICollection<Patient> Patients { get; set; }= new HashSet<Patient>();	


	}
}
