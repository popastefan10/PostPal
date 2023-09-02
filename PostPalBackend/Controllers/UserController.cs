using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.PostService;
using PostPalBackend.Services.UserService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UserController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly IPostService _postService;

		public UserController(IMapper mapper, IUserService userService, IPostService postService)
		{
			_mapper = mapper;
			_userService = userService;
			_postService = postService;
		}

		[HttpPost("register")]
		public IActionResult Register(UserRegisterRequestDTO dto)
		{
			var userResponse = _userService.Register(dto);
			if (userResponse == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, "Something went wrong during register! Please try again.");
			}

			return Ok(userResponse);
		}

		[HttpPost("login")]
		public IActionResult Authenticate(UserAuthRequestDTO userRequest)
		{
			var userResponse = _userService.Authenticate(userRequest);
			if (userResponse == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, "Username or password is invalid.");
			}

			return Ok(userResponse);
		}

		[HttpPost("is-token-valid")]
		public bool IsValidToken(UserIsTokenValidDTO dto)
		{
			return _userService.IsValidToken(dto.Token);
		}

		[HttpPost("{userId}/ban")]
		[Authorization(Role.Admin)]
		public IActionResult Ban([FromRoute] Guid userId)
		{
			var user = _userService.GetById(userId);
			if (user == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");
			}

			return Ok(_userService.Ban(user));
		}

		[HttpPost("{userId}/remove-ban")]
		[Authorization(Role.Admin)]
		public IActionResult RemoveBan([FromRoute] Guid userId)
		{
			var user = _userService.GetById(userId);
			if (user == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");
			}

			return Ok(_userService.RemoveBan(user));
		}


		[HttpGet()]
		[Authorization(Role.Admin)]
		public List<User> GetAll()
		{
			return _userService.GetAllUsers();
		}

		[HttpGet("me")]
		[Authorization(Role.User, Role.Admin)]
		public User GetMe()
		{
			var user = this.GetUserFromHttpContext();

			return user;
		}

		[HttpGet("{userId}")]
		[Authorization(Role.User, Role.Admin)]
		public User GetById([FromRoute] Guid userId)
		{
			var user = _userService.GetById(userId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");

			return user;
		}

		[HttpGet("{id}/posts")]
		public List<PostResponseDTO> GetPosts([FromRoute(Name = "id")] Guid userId)
		{
			var user = _userService.GetById(userId) ?? throw new ProjectException(ProjectStatusCodes.Code.Http404NotFound, "User not found.");

			return _postService.GetAllByUserId(userId).Select(_mapper.Map<PostResponseDTO>).ToList(); // Maybe add Posts navigation property on profile
		}

		[HttpPatch("{userId}")]
		[Authorization(Role.User, Role.Admin)]
		public User Update([FromRoute] Guid userId, [FromBody] UserUpdateDTO dto)
		{
			return this._userService.Update(userId, dto);
		}

		[HttpDelete("me")]
		[Authorization(Role.User, Role.Admin)]
		public User DeleteMe()
		{
			var user = this.GetUserFromHttpContext();

			return this._userService.Delete(user.Id);
		}

		[HttpDelete("{userId}")]
		[Authorization(Role.Admin)]
		public User Delete([FromRoute] Guid userId)
		{
			return this._userService.Delete(userId);
		}
	}
}
