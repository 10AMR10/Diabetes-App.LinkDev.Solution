using DiabetesApp.Core.Enitities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Data.config
{
	internal class PhysiologicalIndicatorsConfigration : IEntityTypeConfiguration<PhysiologicalIndicators>
	{
		public void Configure(EntityTypeBuilder<PhysiologicalIndicators> builder)
		{
			builder.Property(e => e.Date)
			.HasConversion(
				v => v.ToDateTime(TimeOnly.MinValue), // Convert DateOnly to DateTime
				v => DateOnly.FromDateTime(v)         // Convert DateTime back to DateOnly
			)
			.HasColumnType("date"); // Use SQL "date" type to store only the date part

			// Configure Time to be stored as TimeSpan
			builder.Property(e => e.Time)
				.HasConversion(
					v => v.ToTimeSpan(),           // Convert TimeOnly to TimeSpan
					v => TimeOnly.FromTimeSpan(v)  // Convert TimeSpan back to TimeOnly
				)
				.HasColumnType("time");
		}
	}
}
