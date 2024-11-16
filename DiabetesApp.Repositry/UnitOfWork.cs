using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.Repositry.contract;
using DiabetesApp.Repositry.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry
{
	public class UnitOfWork : IUnitOfWork
	{
		private readonly HospitailContext _hospitailContext;
		private Hashtable _repo;
        public UnitOfWork(HospitailContext hospitailContext)
        {
			_repo = new Hashtable();
			this._hospitailContext = hospitailContext;
		}
		public IGenericRepositry<T> GetRepo<T>() where T : BaseEntity
		{
			var name = typeof(T).Name;
			if (!_repo.ContainsKey(name))
			{
				var repo= new GenericRepositry<T>(_hospitailContext);
				_repo.Add(name, repo);
			}
			return _repo[name] as IGenericRepositry<T>;
		}
        public async Task<int> CompeleteAsync()
			=> await _hospitailContext.SaveChangesAsync();

		public async ValueTask DisposeAsync()
			=> await _hospitailContext.DisposeAsync();

	}
}
