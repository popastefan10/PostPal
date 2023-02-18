using PostPalBackend.Helpers.Jwt;
using PostPalBackend.Services.UserService;

namespace PostPalBackend.Helpers.Middleware
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;

		public JwtMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task InvokeAsync(HttpContext httpContext, IJwtUtils jwtUtils, IUserService userService)
		{
			var token = httpContext.Request.Headers.Authorization.FirstOrDefault()?.Split("").Last();
			var userId = jwtUtils.ValidateJwtToken(token);

			if (userId != Guid.Empty)
			{
				httpContext.Items["User"] = userService.GetById(userId);
			}

			await _next(httpContext);
		}
	}
}
