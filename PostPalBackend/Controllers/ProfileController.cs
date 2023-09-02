using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Extensions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.ProfileService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/profiles")]
	public class ProfileController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IProfileService _profileService;

		public ProfileController(IMapper mapper, IProfileService profileService)
		{
			_mapper = mapper;
			_profileService = profileService;
		}

		[HttpPost()]
		[Authorization(Role.User, Role.Admin)]
		public UserProfile Create([FromForm] ProfileCreateDTO dto)
		{
			var user = this.GetUserFromHttpContext();
			var profile = _profileService.Create(dto, user.Id);

			return profile;
		}

		[HttpPost("has-profile")]
		[Authorization(Role.User, Role.Admin)]
		public bool HasProfile()
		{
			var user = this.GetUserFromHttpContext();
			var profile = _profileService.GetByUserId(user.Id);

			return profile != null;
		}

		[HttpGet()]
		[Authorization(Role.User, Role.Admin)]
		public List<UserProfile> GetAll()
		{
			var profiles = _profileService.GetAll();

			return profiles;
		}

		[HttpGet("me")]
		[Authorization(Role.User, Role.Admin)]
		public UserProfile GetMe()
		{
			var user = this.GetUserFromHttpContext();
			var profile = _profileService.GetByUserId(user.Id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Profile not found.");

			return profile;
		}

		[HttpGet("{idsString}")]
		public List<UserProfile> GetByIds([FromRoute] string idsString)
		{
			Guid[] ids = idsString.Split(',').Select(Guid.Parse).ToArray();
			var profiles = _profileService.GetByIds(ids);

			return profiles;
		}

		[HttpPatch("me")]
		[Authorization(Role.User, Role.Admin)]
		public UserProfile Update([FromForm] ProfileUpdateDTO dto)
		{
			var user = this.GetUserFromHttpContext();
			var profile = _profileService.GetByUserId(user.Id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Profile not found.");
			if (user.Id != profile.UserId)
			{
				throw new ProjectException(ProjectStatusCodes.Http401Unauthorized, "You are not the owner of this profile.");
			}
			_profileService.Update(profile, dto);

			return profile;
		}

		[HttpDelete("me")]
		[Authorization(Role.User, Role.Admin)]
		public UserProfile DeleteMe()
		{
			var user = this.GetUserFromHttpContext();
			var profile = _profileService.GetByUserId(user.Id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Profile not found.");
			_profileService.Delete(profile);

			return profile;
		}
	}
}
