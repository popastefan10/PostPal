using PostPalBackend.Models;

namespace PostPalBackend.Helpers.Jwt
{
	public interface IJwtUtils
	{
		public string GenerateJwtToken(User user);
		public Guid ValidateJwtToken(string? token);
	}
}
