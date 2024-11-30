using System.Security.Principal;

namespace DiabetesApp.API.Dtos
{
	public class UserDto
	{
        public string Id { get; set; }
        public string UserName { get; set; }
		    
		public string Email { get; set; }
        public string Token { get; set; }
        public int? HospitalId { get; set; }
		public string? HospitalName { get; set; }

		public string Role { get; set; }
    }
}
