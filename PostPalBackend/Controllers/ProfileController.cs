using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
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
		public ProfileResponseDTO Create(ProfileCreateDTO dto)
		{
			var profile = _profileService.Create(dto);

			return _mapper.Map<ProfileResponseDTO>(profile);
		}

		[HttpGet()]
		[Authorization(Role.User, Role.Admin)]
		public List<ProfileResponseDTO> GetAll()
		{
			var profiles = _profileService.GetAll();

			return profiles.Select(p => _mapper.Map<ProfileResponseDTO>(p)).ToList();
		}

		[HttpGet("{idsString}")]
		[Authorization(Role.User, Role.Admin)]
		public List<ProfileResponseDTO> GetByIds([FromRoute] string idsString)
		{
			Guid[] ids = idsString.Split(',').Select(Guid.Parse).ToArray();
			var profiles = _profileService.GetByIds(ids);

			return profiles.Select(p => _mapper.Map<ProfileResponseDTO>(p)).ToList();
		}

		[HttpPatch("{id}")]
		[Authorization(Role.User, Role.Admin)]
		public ProfileResponseDTO Update([FromRoute] Guid id, [FromBody] ProfileUpdateDTO dto)
		{
			var profile = _profileService.Update(id, dto);

			return _mapper.Map<ProfileResponseDTO>(profile);
		}
	}
}
