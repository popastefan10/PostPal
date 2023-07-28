using System.ComponentModel.DataAnnotations;

namespace PostPalBackend.Models.DTOs.UserDTO
{
	public class UserAuthRequestDTO
	{
		[Required]
		public string Email { get; set; }

		[Required]
		public string Password { get; set; }

		public UserAuthRequestDTO(string email, string password)
		{
			Email = email;
			Password = password;
		}
	}
}
