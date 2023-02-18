using PostPalBackend.Models.Enums;
using PostPalBackend.Models;

namespace PostPalBackend.Models.DTOs.UserDTO {
	public class UserResponseDTO {
		public Guid Id { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Username { get; set; }
		public Role Role { get; set; }

		public string Token { get; set; }

		public UserResponseDTO(User user, string token) {
			Id = user.Id;
			FirstName = user.FirstName;
			LastName = user.LastName;
			Email = user.Email;
			Username = user.Username;
			Token = token;
		}
	}
}
