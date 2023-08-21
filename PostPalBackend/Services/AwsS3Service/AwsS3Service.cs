using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.CredentialManagement;
using Amazon.S3;
using Amazon.S3.Model;
using PostPalBackend.Helpers.Exceptions;

namespace PostPalBackend.Services.AwsS3Service
{
	public class AwsS3Service : IAwsS3Service
	{
		private static readonly string bucketName = "post-pal";
		private static readonly string profileName = "aws-s3-dev";
		private static readonly RegionEndpoint bucketRegion = RegionEndpoint.EUNorth1;
		private readonly IAmazonS3 _client = null!;

		public AwsS3Service()
		{
			var chain = new CredentialProfileStoreChain();
			if (chain.TryGetAWSCredentials(profileName, out AWSCredentials awsCredentials))
			{
				_client = new AmazonS3Client(awsCredentials, bucketRegion);
			}

		}

		public async Task<string> UploadFile(IFormFile file, string filePathInS3)
		{
			try
			{
				var request = new PutObjectRequest
				{
					BucketName = bucketName,
					Key = filePathInS3,
					InputStream = file.OpenReadStream(),
					StorageClass = S3StorageClass.Standard
				};

				var response = await _client.PutObjectAsync(request);

				return string.Format("https://{0}.s3.{1}.amazonaws.com/{2}", bucketName, bucketRegion.SystemName, filePathInS3);
			}
			catch (AmazonS3Exception e)
			{
				throw new ProjectException(
					ProjectStatusCodes.Http500InternalServerError,
					string.Format("Error encountered ***. Message:'{0}' when writing an object", e.Message));
			}
			catch (Exception e)
			{
				throw new ProjectException(
					ProjectStatusCodes.Http500InternalServerError,
					string.Format("Unknown encountered on server. Message:'{0}' when writing an object", e.Message));
			}
		}
	}
}
