using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Services.AwsS3Service;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("/api/dev")]
	public class DevController : ControllerBase
	{
		private readonly IAwsS3Service _awsS3Service;

		public DevController(IAwsS3Service awsS3Service)
		{
			_awsS3Service = awsS3Service;
		}

		[HttpPost("upload-file")]
		public async Task<IActionResult> UploadFile(IFormFile file)
		{
			var fileUrl = await _awsS3Service.UploadFile(file, "test");

			return Ok(fileUrl);
		}
	}
}
