using DiabetesApp.Core.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Data.config
{
	public class PatientConfigration : IEntityTypeConfiguration<Patient>
	{
		public void Configure(EntityTypeBuilder<Patient> builder)
		{
			builder.HasMany(x => x.PhysiologicalIndicatorsList)
				.WithOne(x => x.Patient)
				.HasForeignKey(x => x.PatientId);
			builder.HasOne(x=> x.Hospital)
				.WithMany(x=> x.Patients)
				.HasForeignKey(x=>x.HospitalId);
		}
	}
}
