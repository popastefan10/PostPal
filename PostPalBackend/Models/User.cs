using Microsoft.EntityFrameworkCore;
using PostPalBackend.Models.Base;
using PostPalBackend.Models.Enums;
using System.Text.Json.Serialization;

namespace PostPalBackend.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public class User : BaseEntity
	{
		public string Email { get; set; } = string.Empty;

		[JsonIgnore]
		public string PasswordHash { get; set; } = string.Empty;

		public Role Role { get; set; } = Role.User;

		public bool? IsBanned { get; set; }

		[JsonIgnore]
		public UserProfile? Profile { get; set; }
	}
}
