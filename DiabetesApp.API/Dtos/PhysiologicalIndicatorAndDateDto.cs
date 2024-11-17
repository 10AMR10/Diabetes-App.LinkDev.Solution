using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;

namespace DiabetesApp.API.Dtos
{
	public class PhysiologicalIndicatorAndDateDto
	{
        public int count { get; set; }
        public DateOnly Date { get; set; }
       
    }
}
