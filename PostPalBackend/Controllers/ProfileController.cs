﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostPalBackend.Helpers.Attributes;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Models.Enums;
using PostPalBackend.Services.PostService;
using PostPalBackend.Services.ProfileService;

namespace PostPalBackend.Controllers
{
	[ApiController]
	[Route("api/profiles")]
	public class ProfileController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly IProfileService _profileService;
		private readonly IPostService _postService;

		public ProfileController(IMapper mapper, IProfileService profileService, IPostService postService)
		{
			_mapper = mapper;
			_profileService = profileService;
			_postService = postService;
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

		[HttpGet("{id}/posts")]
		public List<PostResponseDTO> GetPosts([FromRoute(Name = "id")] Guid profileId)
		{
			var profile = _profileService.GetById(profileId);
			if (profile == null)
			{
				throw new ProjectException(ProjectStatusCodes.Code.Http404NotFound, "Profile not found.");
			}

			return _postService.GetAllByProfileId(profileId).Select(_mapper.Map<PostResponseDTO>).ToList(); // Maybe add Posts navigation property on profile
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
