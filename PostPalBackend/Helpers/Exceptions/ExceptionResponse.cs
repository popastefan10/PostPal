using System.Text.Json;

namespace PostPalBackend.Helpers.Exceptions
{
	public class ExceptionResponse
	{
		public ProjectStatusCodes.Code Code { get; }
		public string CodeName { get; }
		public string Details { get; }

		public ExceptionResponse(ProjectStatusCodes.Code code, string details)
		{
			Code = code;
			CodeName = code.ToString();
			Details = details;
		}

		public override string ToString()
		{
			return JsonSerializer.Serialize(this);
		}
	}
}
