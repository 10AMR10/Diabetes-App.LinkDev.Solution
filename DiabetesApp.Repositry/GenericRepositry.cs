using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Repositry.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry
{
	public class GenericRepositry<T> : IGenericRepositry<T> where T : BaseEntity
	{
		private readonly HospitailContext _hospitalContext;

		public GenericRepositry(HospitailContext hospitalContext)
        {
			_hospitalContext = hospitalContext;
		}



		public async Task<IEnumerable<T>?> GetAllAsync()
		{
			if (typeof(T) == typeof(Patient))
			{
				return (IEnumerable<T>)await _hospitalContext.patients.AsNoTracking().ToListAsync();
			}
			return null;
		}

		public async Task<T?> GetAsync(string name)
		{
			if (typeof(T) == typeof(Patient))
				return  await _hospitalContext.Set<Patient>().Where(x=> x.Name==name).Include(x => x.PhysiologicalIndicatorsList).AsNoTracking().FirstOrDefaultAsync() as T;
			return null;
		}

	}
}
