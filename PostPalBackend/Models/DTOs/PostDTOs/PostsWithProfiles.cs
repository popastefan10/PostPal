namespace PostPalBackend.Models.DTOs.PostDTOs
{
	public class PostsWithProfiles
	{
		public List<Post> Posts { get; set; } = null!;

		public Dictionary<Guid, UserProfile> Profiles { get; set; } = null!;
	}
}
