using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.UserService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/users")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("register")]
		public IActionResult Register(UserRegisterRequestDTO dto)
		{
			var userResponse = _userService.Register(dto);
			if (userResponse == null)
			{
				return new BadRequestObjectResult("Something went wrong during register! Please try again.");
			}

			return Ok(userResponse);
		}

		[HttpPost("login")]
		public IActionResult Authenticate(UserAuthRequestDTO userRequest)
		{
			var userResponse = _userService.Authenticate(userRequest);
			if (userResponse == null)
			{
				return new BadRequestObjectResult("Username or password is invalid.");
			}

			return Ok(userResponse);
		}

		[HttpPost("{userId}/ban")]
		[Authorization(Role.Admin)]
		public IActionResult Ban([FromRoute] Guid userId)
		{
			var user = _userService.GetById(userId);
			if (user == null)
			{
				return NotFound("User not found.");
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
				return NotFound("User not found.");
			}

			return Ok(_userService.RemoveBan(user));
		}


		[HttpGet()]
		[Authorization(Role.Admin)]
		public List<User> GetAll()
		{
			return _userService.GetAllUsers();
		}

		[HttpGet("{userId}")]
		[Authorization(Role.User, Role.Admin)]
		public User GetById([FromRoute] Guid userId)
		{
			var user = _userService.GetById(userId);
			if (user == null)
			{
				throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");
			}

			return user;
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
