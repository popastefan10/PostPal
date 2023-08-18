using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Repositories.PostRepository;

namespace PostPalBackend.Services.PostService
{
	public class PostService : IPostService
	{
		private readonly IMapper _mapper;
		private readonly IPostRepository _postRepository;

		public PostService(IMapper mapper, IPostRepository postRepository)
		{
			_mapper = mapper;
			_postRepository = postRepository;
		}

		public Post Create(PostCreateDTO dto, Guid userId)
		{
			var post = _mapper.Map<Post>(dto);
			post.UserId = userId;
			_postRepository.Create(post);
			try
			{
				_postRepository.Save();
			}
			catch (DbUpdateException)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, "Unknown profile.");
			}

			return post;
		}

		public List<Post> GetAll()
		{
			return _postRepository.GetAll();
		}

		public List<Post> GetAllByUserId(Guid userId)
		{
			return _postRepository.GetAll().Where(post => post.UserId == userId).ToList();
		}

		public Post? GetById(Guid id, bool includeProperties = false)
		{
			return includeProperties == false ? _postRepository.FindById(id) : _postRepository.FindByIdIncludeNavigationProperties(id);
		}

		public void Update(Post post, PostUpdateDTO dto)
		{
			if (dto.Description != null)
			{
				post.Description = dto.Description;
			}
			if (dto.ImagesUrls != null)
			{
				post.ImagesUrls = dto.ImagesUrls;
			}
			_postRepository.Update(post);
			_postRepository.Save();
		}

		public void Delete(Post post)
		{
			_postRepository.Delete(post);
			_postRepository.Save();
		}
	}
}
