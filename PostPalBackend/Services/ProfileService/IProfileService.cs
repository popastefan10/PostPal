using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.ProfileDTOs;

namespace PostPalBackend.Services.ProfileService
{
	public interface IProfileService
	{
		UserProfile Create(ProfileCreateDTO dto, Guid userId);

		List<UserProfile> GetAll();

		List<UserProfile> GetByIds(Guid[] ids);

		UserProfile? GetById(Guid id);

		UserProfile? GetByUserId(Guid userId);

		void Update(UserProfile profile, ProfileUpdateDTO dto);

		void Delete(UserProfile profile);
	}
}
