using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities
{
	public class Patient
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int Age { get; set; }
		public string Gender { get; set; }
		public string PhoneNumber { get; set; }
		public string MaritalStatus { get; set; }
		public bool IsPregnant { get; set; }
		public List<PhysiologicalIndicators> PhysiologicalIndicatorsList { get; set; }
	}
}
