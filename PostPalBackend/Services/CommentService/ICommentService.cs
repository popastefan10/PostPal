using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.CommentDTOs;

namespace PostPalBackend.Services.CommentService
{
	public interface ICommentService
	{
		public Comment Create(CommentCreateDTO dto);

		public Comment Delete(Guid id);
	}
}
