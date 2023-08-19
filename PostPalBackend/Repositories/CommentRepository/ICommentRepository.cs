using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.CommentRepository
{
	public interface ICommentRepository : IGenericRepository<Comment>
	{
		public Comment? GetWithProfileById(Guid id);

		public List<Comment> GetByPostId(Guid postId);

		public List<Comment> GetWithProfilesByPostId(Guid postId);
	}
}
