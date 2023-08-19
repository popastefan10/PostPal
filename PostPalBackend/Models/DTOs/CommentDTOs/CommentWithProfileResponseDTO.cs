using PostPalBackend.Models.DTOs.ProfileDTOs;

namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentWithProfileResponseDTO
	{
		public CommentResponseDTO Comment { get; set; } = null!;

		public ProfileResponseDTO? Profile { get; set; } = null!;
	}
}
