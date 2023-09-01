using PostPalBackend.Helpers.Attributes;

namespace PostPalBackend.Helpers.Exceptions
{
	// Keep in sync with frontend implementation
	public static class ProjectStatusCodes
	{
		public enum Code
		{
			[HttpStatusCode(StatusCodes.Status400BadRequest)]
			Http400BadRequest,

			[HttpStatusCode(StatusCodes.Status401Unauthorized)]
			Http401Unauthorized,

			[HttpStatusCode(StatusCodes.Status403Forbidden)]
			Http403Forbidden,

			[HttpStatusCode(StatusCodes.Status404NotFound)]
			Http404NotFound,

			[HttpStatusCode(StatusCodes.Status500InternalServerError)]
			Http500InternalServerError,

			[HttpStatusCode(StatusCodes.Status403Forbidden)]
			UserBanned,

			[HttpStatusCode(StatusCodes.Status400BadRequest)]
			UserAlreadyHasProfile
		}

		public const Code Http400BadRequest = Code.Http400BadRequest;

		public const Code Http401Unauthorized = Code.Http401Unauthorized;

		public const Code Http403Forbidden = Code.Http403Forbidden;

		public const Code Http404NotFound = Code.Http404NotFound;

		public const Code Http500InternalServerError = Code.Http500InternalServerError;

		public const Code UserBanned = Code.UserBanned;

		public const Code UserAlreadyHasProfile = Code.UserAlreadyHasProfile;

		public static int GetHttpStatusCode(Code code)
		{
			var fieldInfo = typeof(Code).GetField(code.ToString());
			var attribute = fieldInfo?.GetCustomAttributes(typeof(HttpStatusCodeAttribute), false)
									   .OfType<HttpStatusCodeAttribute>()
									   .FirstOrDefault();

			return attribute?.StatusCode ?? StatusCodes.Status500InternalServerError;
		}
	}
}
