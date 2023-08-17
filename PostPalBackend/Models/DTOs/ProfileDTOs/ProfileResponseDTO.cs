using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostPalBackend.Models.DTOs.ProfileDTOs
{
	public class ProfileResponseDTO
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public Guid UserId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string? ProfilePictureUrl { get; set; }

		public string? Bio { get; set; }
	}
}
