using System.ComponentModel.DataAnnotations;

namespace PostPalBackend.Models.DTOs.UserDTO {
	public class UserRequestDTO {
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public UserRequestDTO(string email, string password) { 
			Email = email;
			Password = password;
		}
	}
}
