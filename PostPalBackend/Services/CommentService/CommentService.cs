using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.CommentDTOs;
using PostPalBackend.Repositories.CommentRepository;

namespace PostPalBackend.Services.CommentService
{
	public class CommentService : ICommentService
	{
		private readonly IMapper _mapper;
		private readonly ICommentRepository _commentRepository;

		public CommentService(IMapper mapper, ICommentRepository commentRepository)
		{
			_mapper = mapper;
			_commentRepository = commentRepository;
		}

		public Comment Create(CommentCreateDTO dto)
		{
			var comment = _mapper.Map<Comment>(dto);
			_commentRepository.Create(comment);
			try
			{
				_commentRepository.Save();
			}
			catch (DbUpdateException ex)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, ex.InnerException?.Message ?? ex.Message);
			}

			return comment;
		}

		public Comment Delete(Guid id)
		{
			var comment = _commentRepository.GetWithProfileById(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "Comment not found");
			_commentRepository.Delete(comment);
			_commentRepository.Save();

			return comment;
		}
	}
}
