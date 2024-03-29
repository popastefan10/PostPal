﻿using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.PostDTOs;

namespace PostPalBackend.Services.PostService
{
	public interface IPostService
	{
		Post Create(PostCreateDTO dto, Guid userId);

		List<Post> GetAll();

		List<Post> GetAllWithProfiles();

		List<Post> GetAllByUserId(Guid userId);

		Post? GetById(Guid id);

		Post? GetWithUser(Guid id);

		void Update(Post post, PostUpdateDTO dto);

		void Delete(Post post);

		void Like(Guid postId, Guid userId);

		void RemoveLike(Guid postId, Guid userId);

		List<UserProfile> GetLikesProfiles(Guid postId);

		int GetLikesCount(Guid postId);

		List<Comment> GetCommentsWithProfiles(Guid postId);

		int GetCommentsCount(Guid postId);
	}
}
