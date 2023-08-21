namespace PostPalBackend.Models.DTOs.ProfileDTOs
{
	public class ProfileUpdateDTO
	{
		public string? FirstName { get; set; }

		public string? LastName { get; set; }

		public IFormFile? ProfilePicture { get; set; }

		public string? Bio { get; set; }
	}
}
