using PostPalBackend.Data;
using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.UserRepository
{
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(PostPalDbContext context) : base(context) { }

		public User? FindByEmail(string email)
		{
			return this.Table.FirstOrDefault(user => user.Email == email);
		}

		public User? FindByUsername(string username)
		{
			return this.Table.FirstOrDefault(user => user.Username == username);
		}
	}
}
