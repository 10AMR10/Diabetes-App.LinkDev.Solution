using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities
{
	public class PhysiologicalIndicators
	{
		public int Id { get; set; }
		//public int PatientId { get; set; }
		public DateTime Date { get; set; }
		public string HealthStatus { get; set; }
		public int GlucoseLevel { get; set; }
		public int BloodPressure { get; set; }
		public double Temperature { get; set; }
	}
}
