using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;

namespace PostPalBackend.Helpers.Extensions
{
	public static class ControllerBaseExtensions
	{
		public static User GetUserFromHttpContext(this ControllerBase controllerBase)
		{
			var user = controllerBase.HttpContext.Items["User"] as User ?? throw new ProjectException(ProjectStatusCodes.Http401Unauthorized, "You are anauthorized to perform this action.");

			return user;
		}
	}
}
