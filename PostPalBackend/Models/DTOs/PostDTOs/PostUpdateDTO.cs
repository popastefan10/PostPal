namespace PostPalBackend.Models.DTOs.PostDTOs
{
	public class PostUpdateDTO
	{
		public string? Description { get; set; }

		public List<string>? ImagesUrls { get; set; }
	}
}
