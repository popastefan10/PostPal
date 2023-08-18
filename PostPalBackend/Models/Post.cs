using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
    public class Post : BaseEntity
    {
        public Guid ProfileId { get; set; }

        public UserProfile Profile { get; set; } = null!;

        public string Description { get; set; } = string.Empty;

        public List<string> ImagesUrls { get; set; } = new List<string>();
    }
}
