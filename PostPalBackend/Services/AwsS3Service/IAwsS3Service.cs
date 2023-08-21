namespace PostPalBackend.Services.AwsS3Service
{
	public interface IAwsS3Service
	{
		Task<string> UploadFile(IFormFile file, string filePathInS3);
	}
}
