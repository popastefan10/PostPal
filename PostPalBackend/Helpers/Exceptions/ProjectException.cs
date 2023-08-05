namespace PostPalBackend.Helpers.Exceptions
{
	public class ProjectException : Exception
	{
		public ProjectStatusCodes.Code Code { get; }
		public string Details { get; }
		public int HttpStatusCode { get; }

		public ProjectException(ProjectStatusCodes.Code code, string details, int httpStatusCode = -1)
		{
			Details = details;
			Code = code;
			HttpStatusCode = httpStatusCode == -1 ?
				HttpStatusCode = ProjectStatusCodes.GetHttpStatusCode(code) :
				HttpStatusCode = (int)httpStatusCode;
		}
	}
}
