namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentWithProfileResponseDTO
	{
		public CommentResponseDTO Comment { get; set; } = null!;

		public UserProfile? Profile { get; set; } = null!;
	}
}
