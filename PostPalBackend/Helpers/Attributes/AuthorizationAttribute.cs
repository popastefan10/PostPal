using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.Enums;

namespace PostPalBackend.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Method)]
	public class AuthorizationAttribute : Attribute, IAuthorizationFilter
	{
		private readonly ICollection<Role> _roles;

		public AuthorizationAttribute(params Role[] roles)
		{
			_roles = roles;
		}

		public void OnAuthorization(AuthorizationFilterContext context)
		{
			var unauthorizedResultObject = new JsonResult(new ExceptionResponse(ProjectStatusCodes.Code.Http401Unauthorized, "Unauthorized.")) { StatusCode = StatusCodes.Status401Unauthorized };

			if (_roles == null)
			{
				context.Result = unauthorizedResultObject;
			}
			else
			{
				User? user = context.HttpContext.Items["User"] as User;
				if (user == null || !_roles.Contains(user.Role))
				{
					context.Result = unauthorizedResultObject;
				}
			}

		}
	}
}
