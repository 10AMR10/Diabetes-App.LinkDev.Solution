using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Core.specificaitons;
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
			
			return await _hospitalContext.Set<T>().ToListAsync(); ;
		}

		public async Task<T?> GetAsync(string name)
		{
			
				return  await _hospitalContext.Set<T>().FindAsync(name);
			
		}
		public async Task<T?> GetByIdAsync(int? id)
		{
			return await _hospitalContext.Set<T>().FindAsync(id);
		}
		public async Task AddAsync(T entity)
			=> await _hospitalContext.Set<T>().AddAsync(entity);

		public void Delete(T entity)
			=> _hospitalContext.Set<T>().Remove(entity);

		public void Update(T entity)
			=> _hospitalContext.Set<T>().Update(entity);

		public async Task<IEnumerable<T>?> GetAllSpecAsync(ISpecificaiton<T> spec)
		{

			return await ApplySpecification(spec).ToListAsync();
		}


		public async Task<T?> GetSpecAsync(ISpecificaiton<T> spec)
		{
			return await ApplySpecification(spec).FirstOrDefaultAsync();
		}
		private IQueryable<T> ApplySpecification(ISpecificaiton<T> spec)
		{
			return SpecificationEvaluator<T>.GetQuery(_hospitalContext.Set<T>(), spec);
		}
	}
}

