using Microsoft.EntityFrameworkCore;
using PostPalBackend.Data;
using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.CommentRepository
{
	public class CommentRepository : GenericRepository<Comment>, ICommentRepository
	{
		public CommentRepository(PostPalDbContext context) : base(context)
		{
		}

		public Comment? GetWithProfileById(Guid id)
		{
			return this.Table.Include(c => c.User).ThenInclude(u => u.Profile).FirstOrDefault(c => c.Id == id);
		}

		public List<Comment> GetByPostId(Guid postId)
		{
			return this.Table.Where(c => c.PostId == postId).ToList();
		}

		public List<Comment> GetWithProfilesByPostId(Guid postId)
		{
			return this.Table.Include(c => c.User).ThenInclude(u => u.Profile).Where(c => c.PostId == postId).ToList();
		}
	}
}
