using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
	public class Post : BaseEntity
	{
		public Guid UserId { get; set; }

		public User User { get; set; } = null!;

		public string Description { get; set; } = string.Empty;

		public List<string> ImagesUrls { get; set; } = new List<string>();

		public List<PostLike> PostLikes { get; set; } = null!;

		public List<User> PostLikesUsers { get; set; } = null!;

		public List<Comment> PostComments { get; set; } = null!;
	}
}
