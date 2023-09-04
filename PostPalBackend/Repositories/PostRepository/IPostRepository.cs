using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.PostRepository
{
	public interface IPostRepository : IGenericRepository<Post>
	{
		Post? GetWithUser(Guid id);

		Post? GetWithLikes(Guid id);

		public Post? GetWithLikesProfiles(Guid id);

		List<Post> GetAllWithProfiles();
	}
}
