namespace PostPalBackend.Models.DTOs.ProfileDTOs
{
	public class ProfileCreateDTO
	{
		public string FirstName { get; set; } = string.Empty;

		public string LastName { get; set; } = string.Empty;

		public IFormFile? ProfilePicture { get; set; }

		public string? Bio { get; set; }
	}
}
