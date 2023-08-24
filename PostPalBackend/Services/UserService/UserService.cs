using AutoMapper;
using PostPalBackend.Helpers.Exceptions;
using PostPalBackend.Helpers.Jwt;
using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;
using PostPalBackend.Models.Enums;
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
			if (_userRepository.FindByEmail(user.Email) != null)
			{
				throw new ProjectException(ProjectStatusCodes.Http400BadRequest, "Email already exists.");
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
			if (user.IsBanned == true)
			{
				throw new ProjectException(ProjectStatusCodes.UserBanned, "Cannot login into a banned account.");
			}
			var token = _jwtUtils.GenerateJwtToken(user);

			return new UserAuthResponseDTO(user, token);
		}

		public User Ban(User user)
		{
			user.IsBanned = true;
			_userRepository.Save();

			return user;
		}

		public User RemoveBan(User user)
		{
			user.IsBanned = false;
			_userRepository.Save();

			return user;
		}

		public User? GetById(Guid id)
		{
			return this._userRepository.FindById(id);
		}

		public List<User> GetAllUsers()
		{
			return this._userRepository.GetAll();
		}

		public User Update(Guid id, UserUpdateDTO dto)
		{
			var user = this._userRepository.FindById(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");

			if (dto.Email != null)
			{
				user.Email = dto.Email;
			}
			if (dto.Role != null)
			{
				user.Role = (Role)dto.Role;
			}
			this._userRepository.Update(user);
			this._userRepository.Save();

			return user;
		}

		public User Delete(Guid id)
		{
			var user = this._userRepository.FindById(id) ?? throw new ProjectException(ProjectStatusCodes.Http404NotFound, "User not found.");

			this._userRepository.Delete(user);
			this._userRepository.Save();

			return user;
		}
	}
}
