using AutoMapper;
using PostPalBackend.Helpers.Jwt;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;
using PostPalBackend.Repositories.UserRepository;
using BCryptNet = BCrypt.Net.BCrypt;

namespace PostPalBackend.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtUtils _jwtUtils;
		private readonly IMapper _mapper;

		public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IMapper mapper)
		{
			_userRepository = userRepository;
			_jwtUtils = jwtUtils;
			_mapper = mapper;
		}

		public UserRegisterResponseDTO? Register(UserRegisterRequestDTO requestDTO)
		{
			var user = _mapper.Map<User>(requestDTO);
			if (user == null)
			{
				return null;
			}
			_userRepository.Create(user);
			_userRepository.Save();

			var response = _mapper.Map<UserRegisterResponseDTO>(user);
			response.Token = _jwtUtils.GenerateJwtToken(user);

			return response;
		}

		public UserAuthResponseDTO? Authenticate(UserAuthRequestDTO requestDTO)
		{
			var user = _userRepository.FindByEmail(requestDTO.Email);
			if (user == null || !BCryptNet.Verify(requestDTO.Password, user.PasswordHash))
			{
				return null;
			}
			var token = _jwtUtils.GenerateJwtToken(user);

			return new UserAuthResponseDTO(user, token);
		}

		public User? GetById(Guid id)
		{
			return this._userRepository.FindById(id);
		}

		public List<User> GetAllUsers()
		{
			return this._userRepository.GetAll();
		}
	}
}
