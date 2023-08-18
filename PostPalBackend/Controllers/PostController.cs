using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.PostService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/posts")]
	public class PostController : Controller
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
		public PostResponseDTO Create(PostCreateDTO dto)
		{
			var user = this.GetUserFromHttpContext();

			return _mapper.Map<PostResponseDTO>(_postService.Create(dto, user.Id));
		}

		[HttpGet("")]
		public List<PostResponseDTO> GetAll()
		{
			return _postService.GetAll().Select(_mapper.Map<PostResponseDTO>).ToList();
		}

		[HttpGet("{id}")]
		public PostResponseDTO GetById([FromRoute] Guid id)
		{
			var post = _postService.GetById(id);
			if (post == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");
			}

			return _mapper.Map<PostResponseDTO>(post);
		}

		[HttpPatch("{id}")]
		[Authorization(Role.User, Role.Admin)]
		public PostResponseDTO Update([FromRoute] Guid id, PostUpdateDTO dto)
		{
			var post = _postService.GetById(id, includeProperties: true);
			if (post == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");
			}
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
			var post = _postService.GetById(id, includeProperties: true);
			if (post == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");
			}
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
