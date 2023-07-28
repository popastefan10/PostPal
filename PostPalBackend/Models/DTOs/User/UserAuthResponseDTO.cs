using PostPalBackend.Models.Enums;
using PostPalBackend.Models;

namespace PostPalBackend.Models.DTOs.UserDTO
{
	public class UserAuthResponseDTO
	{
		public Guid Id { get; set; }
		public string Email { get; set; }
		public Role Role { get; set; }

		public string Token { get; set; }

		public UserAuthResponseDTO(User user, string token)
		{
			Id = user.Id;
			Email = user.Email;
			Role = user.Role;
			Token = token;
		}
	}
}
