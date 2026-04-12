namespace DiabetesApp.API.Dtos
{
	public class UserToReturnDto
	{
        public string Id { get; set; }
        public string UserName { get; set; }
		
		public string Email { get; set; }
		
		public string? HospitalName { get; set; }
        public int? HospitalId { get; set; }
        public string Role { get; set; }
    }
}
