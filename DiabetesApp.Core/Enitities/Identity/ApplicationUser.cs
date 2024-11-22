using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiabetesApp.Core.Enitities.Identity
{
	public class ApplicationUser:IdentityUser
	{
		// navigitional property with hospital
		public Hospitail?	Hospitail { get; set; }
        public int? HospitalId { get; set; }
    }
}
