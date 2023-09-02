namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentsWithProfilesResponseDTO
	{
		public List<CommentResponseDTO> Comments { get; set; } = new List<CommentResponseDTO>();

		public Dictionary<Guid, UserProfile> Profiles { get; set; } = new Dictionary<Guid, UserProfile>();
	}
}
