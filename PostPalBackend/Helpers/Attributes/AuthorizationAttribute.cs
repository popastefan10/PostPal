using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;

namespace PostPalBackend.Helpers.Attributes
{
	public class AuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		private readonly ICollection<Role> _roles;

		public AuthorizationAttribute(params Role[] roles)
		{
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var unauthorizedStatusObject = new JsonResult(new { Message = "Unauthorized" })
			{ StatusCode = StatusCodes.Status401Unauthorized };

			if (_roles == null)
			{
				context.Result = unauthorizedStatusObject;
			}
			else
			{
				User? user = context.HttpContext.Items["User"] as User;
				if (user == null || !_roles.Contains(user.Role))
				{
					context.Result = unauthorizedStatusObject;
				}
			}

		}
	}
}
