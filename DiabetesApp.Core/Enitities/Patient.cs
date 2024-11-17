using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities
{
	public class Patient:BaseEntity
	{

		public string Name { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
		[Phone]
		public string PhoneNumber { get; set; }
		public int BirthCount { get; set; }
		
		public bool IsPregnant { get; set; }
		public string Address { get; set; } 

		public Hospitail Hospital { get; set; }
        public int HospitalId { get; set; }
        public string? LatestHealthStatus { get; set; }
		public string? Code { get; set; }


		public ICollection<PhysiologicalIndicators>? PhysiologicalIndicatorsList { get; set; }= new HashSet<PhysiologicalIndicators>();	
	}

}
