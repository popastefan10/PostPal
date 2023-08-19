using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.CommentDTOs;
using PostPalBackend.Models.DTOs.PostDTOs;
using PostPalBackend.Models.DTOs.ProfileDTOs;
using PostPalBackend.Models.DTOs.UserDTOs;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PostPalBackend.Helpers.Mappers
{
	public class MapperProfile : AutoMapper.Profile
	{
		public MapperProfile()
		{
			CreateMap<UserRegisterRequestDTO, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCryptNet.HashPassword(src.Password, BCrypt.Net.SaltRevision.Revision2B)));
			CreateMap<User, UserRegisterResponseDTO>();

			CreateMap<ProfileCreateDTO, UserProfile>();
			CreateMap<UserProfile, ProfileResponseDTO>();

			CreateMap<PostCreateDTO, Post>();
			CreateMap<Post, PostResponseDTO>();

			CreateMap<CommentCreateDTO, Comment>();
			CreateMap<Comment, CommentResponseDTO>();
		}
	}
}
