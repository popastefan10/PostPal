namespace PostPalBackend.Models.DTOs.UserDTOs
{
	public class UserRegisterRequestDTO
	{
		public string Email { get; set; }

		public string Password { get; set; }

		public UserRegisterRequestDTO(string Email, string Password)
		{
			this.Email = Email;
			this.Password = Password;
		}
	}
}
