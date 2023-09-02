using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.CommentDTOs;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.PostService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/posts")]
	public class PostController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IPostService _postService;

		public PostController(IMapper mapper, IPostService postService)
		{
			_mapper = mapper;
			_postService = postService;
		}

		[HttpPost("")]
		[Authorization(Role.User, Role.Admin)]
		public PostResponseDTO Create([FromForm] PostCreateDTO dto)
		{
			var user = this.GetUserFromHttpContext();

			return _mapper.Map<PostResponseDTO>(_postService.Create(dto, user.Id));
		}

		[HttpPost("{id}/like")]
		[Authorization(Role.User, Role.Admin)]
		public IActionResult Like([FromRoute] Guid id)
		{
			var user = this.GetUserFromHttpContext();
			_postService.Like(id, user.Id);

			return Ok();
		}

		[HttpPost("{id}/remove-like")]
		[Authorization(Role.User, Role.Admin)]
		public IActionResult RemoveLike([FromRoute] Guid id)
		{
			var user = this.GetUserFromHttpContext();
			_postService.RemoveLike(id, user.Id);

			return Ok();
		}

		[HttpGet("")]
		public List<PostResponseDTO> GetAll()
		{
			return _postService.GetAll().Select(_mapper.Map<PostResponseDTO>).ToList();
		}

		[HttpGet("{id}")]
		public PostResponseDTO GetById([FromRoute] Guid id)
		{
			var post = _postService.GetById(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			return _mapper.Map<PostResponseDTO>(post);
		}

		[HttpGet("{id}/likes")]
		public List<UserProfile> GetLikesProfiles([FromRoute] Guid id)
		{
			return _postService.GetLikesProfiles(id).Select(_mapper.Map<UserProfile>).ToList();
		}

		[HttpGet("{id}/likes/count")]
		public int GetLikesCount([FromRoute] Guid id)
		{
			return _postService.GetLikesCount(id);
		}

		[HttpGet("{id}/comments")]
		public CommentsWithProfilesResponseDTO GetComments([FromRoute] Guid id)
		{
			var comments = _postService.GetCommentsWithProfiles(id);

			return new CommentsWithProfilesResponseDTO
			{
				Comments = comments.Select(_mapper.Map<CommentResponseDTO>).ToList(),
				Profiles = comments.Select(c => c.User.Profile).Distinct().Select(_mapper.Map<UserProfile>).ToDictionary(p => p.Id)
			};
		}

		[HttpGet("{id}/comments/count")]
		public int GetCommentsCount([FromRoute] Guid id)
		{
			return _postService.GetCommentsCount(id);
		}

		[HttpPatch("{id}")]
		[Authorization(Role.User, Role.Admin)]
		public PostResponseDTO Update([FromRoute] Guid id, [FromForm] PostUpdateDTO dto)
		{
			var post = _postService.GetWithUser(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");
			var user = this.GetUserFromHttpContext();
			if (post.User.Id != user.Id)
			{
				throw new ProjectException(ProjectStatusCodes.Http403Forbidden, "You are not the creator of this post.");
			}
			_postService.Update(post, dto);

			return _mapper.Map<PostResponseDTO>(post);
		}

		[HttpDelete("{id}")]
		[Authorization(Role.User, Role.Admin)]
		public PostResponseDTO Delete([FromRoute] Guid id)
		{
			var post = _postService.GetWithUser(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");
			var user = this.GetUserFromHttpContext();
			if (user.Role == Role.User && post.User.Id != user.Id)
			{
				throw new ProjectException(ProjectStatusCodes.Http403Forbidden, "You are not the creator of this post.");
			}
			_postService.Delete(post);

			return _mapper.Map<PostResponseDTO>(post);
		}
	}
}
