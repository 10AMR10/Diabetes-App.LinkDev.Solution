using DiabetesApp.Core.Enitities;
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
	}
}
