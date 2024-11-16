﻿using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.specificaitons.patients
{
	public class PatientWithAllDataSpec:BaseSpecification<Patient>
	{
		public PatientWithAllDataSpec():base()
		{
			GetIncludes();
		}

		public PatientWithAllDataSpec(string name) :base(x=> x.Name == name)
		{
			GetIncludes();
		}
		

		private void GetIncludes()
		{
			Includes.Add(x => x.Hospital);
			Includes.Add(x => x.PhysiologicalIndicatorsList);
		}
	}
}