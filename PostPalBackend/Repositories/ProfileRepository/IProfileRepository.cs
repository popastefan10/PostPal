using PostPalBackend.Models;
using PostPalBackend.Repositories.GenericRepository;

namespace PostPalBackend.Repositories.ProfileRepository
{
	public interface IProfileRepository : IGenericRepository<UserProfile>
	{
		List<UserProfile> GetByIds(Guid[] ids);

		UserProfile? GetByUserId(Guid userId);
	}
}
