using PostPalBackend.Data;
using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.ProfileRepository
{
	public class ProfileRepository : GenericRepository<UserProfile>, IProfileRepository
	{
		public ProfileRepository(PostPalDbContext context) : base(context)
		{
		}

		public List<UserProfile> GetByIds(Guid[] ids)
		{
			return Table.Where(p => ids.Contains(p.Id)).ToList();
		}

		public UserProfile? GetByUserId(Guid userId)
		{
			return Table.FirstOrDefault(p => p.UserId == userId);
		}
	}
}
