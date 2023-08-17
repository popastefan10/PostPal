using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.ProfileDTOs;

namespace PostPalBackend.Services.ProfileService
{
	public interface IProfileService
	{
		UserProfile Create(ProfileCreateDTO dto);

		List<UserProfile> GetAll();

		List<UserProfile> GetByIds(Guid[] ids);

		UserProfile Update(Guid id, ProfileUpdateDTO dto);
	}
}
