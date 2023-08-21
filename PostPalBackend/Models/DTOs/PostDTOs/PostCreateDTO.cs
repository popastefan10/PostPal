namespace PostPalBackend.Models.DTOs.PostDTOs
{
	public class PostCreateDTO
	{
		public string Description { get; set; } = string.Empty;

		public List<IFormFile> Images { get; set; } = new List<IFormFile>();
	}
}
