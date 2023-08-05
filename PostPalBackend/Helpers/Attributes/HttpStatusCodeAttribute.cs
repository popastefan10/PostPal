namespace PostPalBackend.Helpers.Attributes
{
	[AttributeUsage(AttributeTargets.Field)]
	public class HttpStatusCodeAttribute : Attribute
	{
		public int StatusCode { get; }

		public HttpStatusCodeAttribute(int statusCode)
		{
			StatusCode = statusCode;
		}
	}
}
