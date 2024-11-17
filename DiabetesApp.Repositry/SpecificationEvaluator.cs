using DiabetesApp.Core.Enitities;
using DiabetesApp.Core.specificaitons;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Repositry
{
	public class SpecificationEvaluator<T> where T : BaseEntity
	{
		public static IQueryable<T> GetQuery(IQueryable<T> set,ISpecificaiton<T> spec)
		{
			var query = set;
			if (spec.OrderBy is not null)
				query = query.OrderBy(spec.OrderBy);
			if (spec.Criteria is not null)
				query= query.Where(spec.Criteria);
			
			query = spec.Includes.Aggregate(query,(start,next) => start.Include(next));
			return query;
		}
	}
}
