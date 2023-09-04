using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostPalBackend.Models.DTOs.CommentDTOs
{
	public class CommentResponseDTO
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }

		public DateTime? DateCreated { get; set; }

		public DateTime? DateModified { get; set; }

		public Guid UserId { get; set; }

		public Guid PostId { get; set; }

		public string Content { get; set; } = string.Empty;
	}
}
