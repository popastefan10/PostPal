using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.PostDTOs;

namespace PostPalBackend.Services.PostService
{
	public interface IPostService
	{
		Post Create(PostCreateDTO dto);

		List<Post> GetAll();

		List<Post> GetAllByProfileId(Guid userId);

		Post? GetById(Guid id, bool includeProperties = false);

		void Update(Post post, PostUpdateDTO dto);

		void Delete(Post post);
	}
}
