using DiabetesApp.Core.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
			// Resolve the seed-data folder relative to the application base directory.
			// This works both locally (bin/Debug/net8.0) and on deployed servers.
			var baseDir = AppDomain.CurrentDomain.BaseDirectory;
			var seedDir = Path.Combine(baseDir, "DataSeeding");

			try
			{
				if (!await _hospitalContext.hospitals.AnyAsync())
				{
					var hospitalsPath = Path.Combine(seedDir, "hospitails.json");
					if (File.Exists(hospitalsPath))
					{
						var hospitalsData = await File.ReadAllTextAsync(hospitalsPath);
						var hospitals = JsonSerializer.Deserialize<List<Hospitail>>(hospitalsData);
						if (hospitals?.Count > 0)
						{
							foreach (var pat in hospitals)
								_hospitalContext.Set<Hospitail>().Add(pat);
							await _hospitalContext.SaveChangesAsync();
						}
					}
				}
				if (!await _hospitalContext.patients.AnyAsync())
				{
					var patientsPath = Path.Combine(seedDir, "Patients.json");
					if (File.Exists(patientsPath))
					{
						var patientsData = await File.ReadAllTextAsync(patientsPath);
						var patients = JsonSerializer.Deserialize<List<Patient>>(patientsData);
						if (patients?.Count > 0)
						{
							foreach (var pat in patients)
								_hospitalContext.Set<Patient>().Add(pat);
							await _hospitalContext.SaveChangesAsync();
						}
					}
				}
				if (!await _hospitalContext.physiologicalIndicators.AnyAsync())
				{
					var physioPath = Path.Combine(seedDir, "physiologicalIndicators.json");
					if (File.Exists(physioPath))
					{
						var physiologicalIndicatorsData = await File.ReadAllTextAsync(physioPath);
						var physiologicalIndicators = JsonSerializer.Deserialize<List<PhysiologicalIndicators>>(physiologicalIndicatorsData);
						if (physiologicalIndicators?.Count > 0)
						{
							foreach (var pat in physiologicalIndicators)
								_hospitalContext.Set<PhysiologicalIndicators>().Add(pat);
							await _hospitalContext.SaveChangesAsync();
						}
					}
				}
			}
			catch (Exception)
			{
				// Seeding is best-effort — if it fails (e.g. files missing on server),
				// the app should still start. The DB may already be seeded from a prior run.
			}
		}
	}
}

