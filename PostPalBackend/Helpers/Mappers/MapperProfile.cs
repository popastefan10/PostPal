using AutoMapper;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTOs;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PostPalBackend.Helpers.Mappers
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<UserRegisterRequestDTO, User>().ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => BCryptNet.HashPassword(src.Password, BCrypt.Net.SaltRevision.Revision2B)));
			CreateMap<User, UserRegisterResponseDTO>();
		}
	}
}
