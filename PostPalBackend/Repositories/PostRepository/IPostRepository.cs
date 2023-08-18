using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.PostRepository
{
	public interface IPostRepository : IGenericRepository<Post>
	{
		Post? FindByIdIncludeNavigationProperties(Guid id);
	}
}
