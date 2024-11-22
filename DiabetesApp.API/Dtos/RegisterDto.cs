using System.ComponentModel.DataAnnotations;

namespace DiabetesApp.API.Dtos
{
	public class RegisterDto
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; }
		[Required]
		[RegularExpression("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+]).{5,}$",
	ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, one special character, and be at least 5 characters long.")]
		public string Password { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public int HospitalId { get; set; }
    }
}
