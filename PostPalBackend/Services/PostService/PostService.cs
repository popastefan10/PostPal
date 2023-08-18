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

		public Post? GetById(Guid id)
		{
			return _postRepository.FindById(id);
		}

		public Post? GetWithUser(Guid id)
		{
			return _postRepository.GetWithUser(id);
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

		public void Like(Guid postId, Guid userId)
		{
			var post = _postRepository.GetWithLikes(postId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			var postLike = post.PostLikes.FirstOrDefault(like => like.UserId == userId);
			if (postLike == null)
			{
				post.PostLikes.Add(new PostLike
				{
					PostId = postId,
					UserId = userId
				});
				_postRepository.Save();
			}
		}

		public void RemoveLike(Guid postId, Guid userId)
		{
			var post = _postRepository.GetWithLikes(postId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			var postLike = post.PostLikes.FirstOrDefault(like => like.UserId == userId);
			if (postLike != null)
			{
				post.PostLikes.Remove(postLike);
				_postRepository.Save();
			}
		}

		public List<UserProfile> GetLikesProfiles(Guid postId)
		{
			var post = _postRepository.GetWithLikesProfiles(postId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			return post.PostLikesUsers.Select(user => user.Profile).Where(profile => profile! != null).ToList() as List<UserProfile>;
		}

		public int GetLikesCount(Guid postId)
		{
			var post = _postRepository.GetWithLikes(postId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			return post.PostLikes.Count;
		}
	}
}
