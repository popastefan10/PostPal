using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PostPalBackend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PostPalBackend.Helpers.Jwt
{
	public class JwtUtils : IJwtUtils
	{
		public readonly AppSettings AppSettings;

		public JwtUtils(IOptions<AppSettings> appSettings)
		{
			AppSettings = appSettings.Value;
		}

		public string GenerateJwtToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var secret = Encoding.ASCII.GetBytes(AppSettings.JwtSecret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					new Claim("id", user.Id.ToString())
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);

			return tokenHandler.WriteToken(token);
		}

		public Guid ValidateJwtToken(string? token)
		{
			if (token == null)
				return Guid.Empty;

			var tokenHandler = new JwtSecurityTokenHandler();
			var secret = Encoding.ASCII.GetBytes(AppSettings.JwtSecret);
			var validationParameters = new TokenValidationParameters
			{
				ValidateIssuer = false,
				ValidateIssuerSigningKey = true,
				ValidateAudience = false,
				ValidateLifetime = true,
				IssuerSigningKey = new SymmetricSecurityKey(secret),
				ClockSkew = TimeSpan.Zero,
			};

			try
			{
				tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

				var jwtToken = (JwtSecurityToken)validatedToken;
				var userId = new Guid(
					jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id", new Claim("id", "")).Value
				);

				return userId;
			}
			catch
			{
				return Guid.Empty;
			}
		}
	}
}
