using Microsoft.EntityFrameworkCore;
using PostPalBackend.Models.Base;
using PostPalBackend.Models.Enums;
using System.Text.Json.Serialization;

namespace PostPalBackend.Models
{
	[Index(nameof(Email), IsUnique = true)]
	public class User : BaseEntity
	{
		public string Email { get; set; }

		[JsonIgnore]
		public string PasswordHash { get; set; }

		public Role Role { get; set; } = Role.User;

		public bool? isBanned { get; set; }
	}
}
