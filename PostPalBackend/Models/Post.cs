using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
	public class Post : BaseEntity
	{
		public Guid ProfileId { get; set; }

		public UserProfile Profile { get; set; }

		public string Description { get; set; }

		public List<string> ImagesUrls { get; set; }
	}
}
