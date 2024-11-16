using DiabetesApp.Core.Enitities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry.Data
{
	public class HospitailContext:DbContext
	{
        public HospitailContext(DbContextOptions<HospitailContext> dbContextOptions):base(dbContextOptions)
        {          
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}
		public DbSet<Hospitail> hospitals { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PhysiologicalIndicators> physiologicalIndicators { get; set; }
    }
}
