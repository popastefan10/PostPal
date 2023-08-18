using PostPalBackend.Models.Base;

namespace PostPalBackend.Models
{
    public class UserProfile : BaseEntity
    {
        public Guid UserId { get; set; }

        public User User { get; set; } = null!;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string? ProfilePictureUrl { get; set; }

        public string? Bio { get; set; }
    }
}
