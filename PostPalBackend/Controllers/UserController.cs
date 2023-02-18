using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Services.UserService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/user")]
	public class UserController : ControllerBase
	{
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

		[HttpPost("authenticate")]
		public IActionResult Authenticate(UserRequestDTO userRequest)
		{
			var userResponse = _userService.Authenticate(userRequest);

			if (userResponse == null)
			{
				return new BadRequestObjectResult("Username or password is invalid!");
			}

			return Ok(userResponse);
		}
	}
}
