using PostPalBackend.Models.DTOs.ProfileDTOs;

namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentsWithProfilesResponseDTO
	{
		public List<CommentResponseDTO> Comments { get; set; } = new List<CommentResponseDTO>();

		public Dictionary<Guid, ProfileResponseDTO> Profiles { get; set; } = new Dictionary<Guid, ProfileResponseDTO>();
	}
}
