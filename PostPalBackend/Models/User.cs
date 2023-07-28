using PostPalBackend.Models.Base;
using PostPalBackend.Models.Enums;
using System.Text.Json.Serialization;

namespace PostPalBackend.Models
{
	public class User : BaseEntity
	{
		public string Email { get; set; }

		[JsonIgnore]
		public string PasswordHash { get; set; }

		public Role Role { get; set; }

		public bool? isBanned { get; set; }
	}
}
