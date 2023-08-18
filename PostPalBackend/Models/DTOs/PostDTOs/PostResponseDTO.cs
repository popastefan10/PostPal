using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostPalBackend.Models.DTOs.PostDTOs
{
	public class PostResponseDTO
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public DateTime? DateCreated { get; set; }

		public DateTime? DateModified { get; set; }

		public Guid ProfileId { get; set; }

		public string Description { get; set; }

		public List<string> ImagesUrls { get; set; }
	}
}
