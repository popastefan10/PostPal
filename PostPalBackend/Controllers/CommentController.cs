using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Models.DTOs.CommentDTOs;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.CommentService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/comments")]
	public class CommentController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly ICommentService _commentService;

		public CommentController(IMapper mapper, ICommentService commentService)
		{
			_mapper = mapper;
			_commentService = commentService;
		}

		[HttpPost()]
		[Authorization(Role.User, Role.Admin)]
		public CommentResponseDTO Create(CommentCreateDTO dto)
		{
			return _mapper.Map<CommentResponseDTO>(_commentService.Create(dto));
		}

		[HttpDelete("{id}")]
		[Authorization(Role.User, Role.Admin)]
		public CommentWithProfileResponseDTO Delete([FromRoute] Guid id)
		{
			var comment = _commentService.Delete(id);

			return new CommentWithProfileResponseDTO
			{
				Comment = _mapper.Map<CommentResponseDTO>(comment),
				Profile = _mapper.Map<ProfileResponseDTO>(comment.User.Profile)
			};
		}
	}
}
