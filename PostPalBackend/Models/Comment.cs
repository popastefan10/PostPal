using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
	public class Comment : BaseEntity
	{
		public Guid UserId { get; set; }

		public User User { get; set; } = null!;

		public Guid PostId { get; set; }

		public string Description { get; set; } = string.Empty;
	}
}
