using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.specificaitons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Repositry.contract
{
	public interface IGenericRepositry<T> where T : BaseEntity
	{
		public Task<IEnumerable<T>?> GetAllAsync();
		public Task<T?> GetAsync(string name);
		public Task<T?> GetByIdAsync(int id);
		public Task<IEnumerable<T>?> GetAllSpecAsync(ISpecificaiton<T> spec);
		public Task<T?> GetSpecAsync(ISpecificaiton<T> spec);
		public Task AddAsync(T entity);
		public void Update(T entity);
		public void Delete(T entity);
	}
}
