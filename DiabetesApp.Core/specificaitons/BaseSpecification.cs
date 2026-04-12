using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.specificaitons
{
	public class BaseSpecification<T> : ISpecificaiton<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get; set; } = null;

		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; } = null;
		public BaseSpecification()
        {
            
        }
		public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
			Criteria=criteria;
		}
	}
}
