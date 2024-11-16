using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Repositry.contract
{
	public interface IUnitOfWork:IAsyncDisposable
	{
		IGenericRepositry<T> GetRepo<T>() where T : BaseEntity;
		Task<int> CompeleteAsync();
	}
}
