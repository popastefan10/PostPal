using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
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

		[HttpPost("login")]
		public IActionResult Authenticate(UserAuthRequestDTO userRequest)
		{
			var userResponse = _userService.Authenticate(userRequest);

			if (userResponse == null)
			{
				return new BadRequestObjectResult("Username or password is invalid!");
			}

			return Ok(userResponse);
		}

		[HttpGet()]
		[Authorization(Role.Admin)]
		public List<User> All()
		{
			return _userService.GetAllUsers();
		}
	}
}
