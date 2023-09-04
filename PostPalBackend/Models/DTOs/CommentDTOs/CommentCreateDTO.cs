namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentCreateDTO
	{
		public Guid UserId { get; set; }

		public Guid PostId { get; set; }

		public string Content { get; set; } = string.Empty;
	}
}
