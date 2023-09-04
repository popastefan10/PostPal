using PostPalBackend.Models.Base;
using System.Text.Json.Serialization;

namespace PostPalBackend.Models
{
	public class Post : BaseEntity
	{
		public Guid UserId { get; set; }

		[JsonIgnore]
		public User User { get; set; } = null!;

		public string Description { get; set; } = string.Empty;

		public List<string> ImagesUrls { get; set; } = new List<string>();

		[JsonIgnore]
		public List<PostLike> PostLikes { get; set; } = null!;

		[JsonIgnore]
		public List<User> PostLikesUsers { get; set; } = null!;

		[JsonIgnore]
		public List<Comment> PostComments { get; set; } = null!;
	}
}
