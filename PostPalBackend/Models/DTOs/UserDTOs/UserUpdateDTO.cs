using PostPalBackend.Models.Enums;

namespace PostPalBackend.Models.DTOs.UserDTOs
{
	public class UserUpdateDTO
	{
		public string? Email { get; set; }

		public Role? Role { get; set; }
	}
}
