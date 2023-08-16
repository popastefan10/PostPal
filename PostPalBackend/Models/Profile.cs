using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
	public class Profile : BaseEntity
	{
		public Guid UserId { get; set; }

		public User User { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string? ProfilePictureUrl { get; set; }

		public string? Bio { get; set; }
	}
}
