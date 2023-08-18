namespace PostPalBackend.Models.DTOs.PostDTOs
{
	public class PostCreateDTO
	{
		public string Description { get; set; } = string.Empty;

		public List<string> ImagesUrls { get; set; } = new List<string>();
	}
}
