using Microsoft.EntityFrameworkCore;
using PostPalBackend.Data;
using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.PostRepository
{
	public class PostRepository : GenericRepository<Post>, IPostRepository
	{
		public PostRepository(PostPalDbContext context) : base(context)
		{
		}

		public Post? GetWithUser(Guid id)
		{
			return this.Table.Include(post => post.User).Where(post => post.Id == id).FirstOrDefault();
		}

		public Post? GetWithLikes(Guid id)
		{
			return this.Table.Include(post => post.PostLikes).Where(post => post.Id == id).FirstOrDefault();
		}

		public Post? GetWithLikesProfiles(Guid id)
		{
			return this.Table.Include(post => post.PostLikesUsers).ThenInclude(user => user.Profile).Where(post => post.Id == id).FirstOrDefault();
		}
	}
}
