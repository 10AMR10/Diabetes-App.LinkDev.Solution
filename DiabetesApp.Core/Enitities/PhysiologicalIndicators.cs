using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities
{
	public class PhysiologicalIndicators:BaseEntity
	{
		
		public DateOnly Date { get; set; }
		public TimeOnly Time { get; set; }
		public string HealthStatus { get; set; }
		public double GlucoseLevel { get; set; }
		public double BloodPressure { get; set; }
		public double Temperature { get; set; }
		
		public Patient Patient { get; set; }
		public int PatientId { get; set; }
	}
}