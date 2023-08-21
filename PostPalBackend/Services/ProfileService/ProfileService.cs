using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Repositories.ProfileRepository;
using PostPalBackend.Services.AwsS3Service;

namespace PostPalBackend.Services.ProfileService
{
	public class ProfileService : IProfileService
	{
		private readonly IMapper _mapper;
		private readonly IProfileRepository _profileRepository;
		private readonly IAwsS3Service _awsS3Service;

		public ProfileService(IMapper mapper, IProfileRepository profileRepository, IAwsS3Service awsS3Service)
		{
			_mapper = mapper;
			_profileRepository = profileRepository;
			_awsS3Service = awsS3Service;
		}

		private Task<string> UploadProfilePicture(IFormFile profilePicture, Guid userId)
		{
			var filePathInS3 = $"users/{userId}/profile-pictures/{userId}";

			return _awsS3Service.UploadFile(profilePicture, filePathInS3);
		}

		public UserProfile Create(ProfileCreateDTO dto, Guid userId)
		{
			var profile = _mapper.Map<UserProfile>(dto);
			profile.UserId = userId;
			if (dto.ProfilePicture != null)
			{
				profile.ProfilePictureUrl = UploadProfilePicture(dto.ProfilePicture, userId).Result;
			}
			_profileRepository.Create(profile);
			try
			{
				_profileRepository.Save();
			}
			catch (DbUpdateException ex)
			{
				throw new ProjectException(ProjectStatusCodes.UserAlreadyHasProfile, ex.InnerException?.Message ?? ex.Message);
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

		public UserProfile? GetByUserId(Guid userId)
		{
			return _profileRepository.GetByUserId(userId);
		}

		public void Update(UserProfile profile, ProfileUpdateDTO dto)
		{
			if (dto.FirstName != null)
			{
				profile.FirstName = dto.FirstName;
			}
			if (dto.LastName != null)
			{
				profile.LastName = dto.LastName;
			}
			if (dto.ProfilePicture != null)
			{
				profile.ProfilePictureUrl = UploadProfilePicture(dto.ProfilePicture, profile.UserId).Result;
			}
			if (dto.Bio != null)
			{
				profile.Bio = dto.Bio;
			}
			_profileRepository.Update(profile);
			_profileRepository.Save();
		}
	}
}
