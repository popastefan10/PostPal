using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.UserRepository {
	public interface IUserRepository : IGenericRepository<User> {
		public User? FindByEmail(string email);
		public User? FindByUsername(string username);
	}
}
