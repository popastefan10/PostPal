using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Repositories.CommentRepository;
using PostPalBackend.Repositories.PostRepository;
using PostPalBackend.Services.AwsS3Service;

namespace PostPalBackend.Services.PostService
{
	public class PostService : IPostService
	{
		private readonly IMapper _mapper;
		private readonly IPostRepository _postRepository;
		private readonly ICommentRepository _commentRepository;
		private readonly IAwsS3Service _awsS3Service;

		public PostService(IMapper mapper, IPostRepository postRepository, ICommentRepository commentRepository, IAwsS3Service awsS3Service)
		{
			_mapper = mapper;
			_postRepository = postRepository;
			_commentRepository = commentRepository;
			_awsS3Service = awsS3Service;
		}

		private async Task<List<string>> UploadImages(List<IFormFile> images, Guid userId, Guid postId)
		{
			List<string> imagesUrls = new();
			// TODO delete uploaded images if one of them fails to upload
			foreach (var image in images)
			{
				var imageId = Guid.NewGuid();
				var filePathInS3 = $"users/{userId}/posts/{postId}/{imageId}";
				var imageUrl = await _awsS3Service.UploadFile(image, filePathInS3);
				imagesUrls.Add(imageUrl);
			}

			return imagesUrls;
		}

		public Post Create(PostCreateDTO dto, Guid userId)
		{
			var post = _mapper.Map<Post>(dto);
			post.UserId = userId;
			_postRepository.Create(post);
			if (dto.Images.Count > 0)
			{
				UploadImages(dto.Images, userId, post.Id).Result.ForEach(imageUrl => post.ImagesUrls.Add(imageUrl));
			}
			try
			{
				_postRepository.Save();
			}
			catch (DbUpdateException ex)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, ex.InnerException?.Message ?? ex.Message);
			}

			return post;
		}

		public List<Post> GetAll()
		{
			return _postRepository.GetAll();
		}

		public List<Post> GetAllWithProfiles()
		{
			return _postRepository.GetAllWithProfiles();
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
			if (dto.Images != null)
			{
				// TODO delete old images
				UploadImages(dto.Images, post.UserId, post.Id).Result.ForEach(imageUrl => post.ImagesUrls.Add(imageUrl));
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

		public List<Comment> GetCommentsWithProfiles(Guid postId)
		{
			_ = _postRepository.FindById(postId) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Post not found.");

			return _commentRepository.GetWithProfilesByPostId(postId);
		}

		public int GetCommentsCount(Guid postId)
		{
			return _commentRepository.GetByPostId(postId).Count;
		}
	}
}
