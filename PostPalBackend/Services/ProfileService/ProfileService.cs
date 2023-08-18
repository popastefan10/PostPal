using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Repositories.ProfileRepository;

namespace PostPalBackend.Services.ProfileService
{
	public class ProfileService : IProfileService
	{
		private readonly IMapper _mapper;
		private readonly IProfileRepository _profileRepository;

		public ProfileService(IMapper mapper, IProfileRepository profileRepository)
		{
			_mapper = mapper;
			_profileRepository = profileRepository;
		}

		public UserProfile Create(ProfileCreateDTO dto)
		{
			var profile = _mapper.Map<UserProfile>(dto);
			_profileRepository.Create(profile);
			try
			{
				_profileRepository.Save();
			}
			catch (DbUpdateException)
			{
				throw new ProjectException(ProjectStatusCodes.UserAlreadyHasProfile, "This user already has a profile.");
			}

			return profile;
		}

		public List<UserProfile> GetAll()
		{
			return _profileRepository.GetAll();
		}

		public List<UserProfile> GetByIds(Guid[] ids)
		{
			return _profileRepository.GetByIds(ids);
		}

		public UserProfile? GetById(Guid id)
		{
			return _profileRepository.FindById(id);
		}

		public UserProfile Update(Guid id, ProfileUpdateDTO dto)
		{
			var profile = _profileRepository.FindById(id) ?? throw new ProjectException(ProjectStatusCodes.Code.Http404NotFound, "Profile not found.");

			if (dto.FirstName != null)
			{
				profile.FirstName = dto.FirstName;
			}
			if (dto.LastName != null)
			{
				profile.LastName = dto.LastName;
			}
			if (dto.ProfilePictureUrl != null)
			{
				profile.ProfilePictureUrl = dto.ProfilePictureUrl;
			}
			if (dto.Bio != null)
			{
				profile.Bio = dto.Bio;
			}
			_profileRepository.Update(profile);
			_profileRepository.Save();

			return profile;
		}
	}
}
