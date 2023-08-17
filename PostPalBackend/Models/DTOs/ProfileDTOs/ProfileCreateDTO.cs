﻿namespace PostPalBackend.Models.DTOs.ProfileDTOs
{
	public class ProfileCreateDTO
	{
		public Guid UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string? ProfilePictureUrl { get; set; }

		public string? Bio { get; set; }
	}
}
