using PostPalBackend.Helpers.Attributes;

namespace PostPalBackend.Helpers.Exceptions
{
	public static class ProjectStatusCodes
	{
		public enum Code
		{
			[HttpStatusCode(StatusCodes.Status401Unauthorized)]
			Http401,

			[HttpStatusCode(StatusCodes.Status500InternalServerError)]
			Http500,

			[HttpStatusCode(StatusCodes.Status403Forbidden)]
			UserBanned
		}

		public const Code Http401 = Code.Http401;

		public const Code Http500 = Code.Http500;

		public const Code UserBanned = Code.UserBanned;

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
