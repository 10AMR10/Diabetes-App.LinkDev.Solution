using DiabetesApp.Core.Enitities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.specificaitons.physiologicalIndicators
{
	public class PhysiologicalIndicatorsSpecification:BaseSpecification<PhysiologicalIndicators>
	{
        public PhysiologicalIndicatorsSpecification():base()
        {
            Includes.Add(x => x.Patient);
            
        }
    }
}
