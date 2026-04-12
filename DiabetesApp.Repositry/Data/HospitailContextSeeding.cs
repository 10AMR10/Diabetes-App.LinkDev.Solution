using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Data
{
	public class HospitailContextSeeding
	{
		public async static Task SeedingAsync(HospitailContext _hospitalContext)
		{
			if (_hospitalContext.hospitals.Count() == 0)
			{
				var hospitalsData = File.ReadAllText("../DiabetesApp.Repositry/Data/DataSeeding/hospitails.json");
				var hospitals = JsonSerializer.Deserialize<List<Hospitail>>(hospitalsData);
				if (hospitals?.Count() > 0)
				{
					foreach (var pat in hospitals)
						_hospitalContext.Set<Hospitail>().Add(pat);
					await _hospitalContext.SaveChangesAsync();
				}
			}
			if (_hospitalContext.patients.Count()==  0)
			{
				var patientsData = File.ReadAllText("../DiabetesApp.Repositry/Data/DataSeeding/Patients.json");
				var patients = JsonSerializer.Deserialize < List < Patient >> (patientsData);
				if (patients?.Count()>0)
				{
					foreach (var pat in patients)
						_hospitalContext.Set<Patient>().Add(pat);
					await _hospitalContext.SaveChangesAsync();
				}
			}
			if (_hospitalContext.physiologicalIndicators.Count() == 0)
			{
				var physiologicalIndicatorsData = File.ReadAllText("../DiabetesApp.Repositry/Data/DataSeeding/physiologicalIndicators.json");
				var physiologicalIndicators = JsonSerializer.Deserialize<List<PhysiologicalIndicators>>(physiologicalIndicatorsData);
				if (physiologicalIndicators?.Count() > 0)
				{
					foreach (var pat in physiologicalIndicators)
						_hospitalContext.Set<PhysiologicalIndicators>().Add(pat);
					await _hospitalContext.SaveChangesAsync();
				}
			}
		}
	}
}
