using BCryptNet = BCrypt.Net.BCrypt;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Repositories.UserRepository;
using PostPalBackend.Models;
using PostPalBackend.Helpers.Jwt;

namespace PostPalBackend.Services.UserService
{
	public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;
		private readonly IJwtUtils _jwtUtils;

		public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
		{
			_userRepository = userRepository;
			_jwtUtils = jwtUtils;
		}

		public UserAuthResponseDTO? Authenticate(UserAuthRequestDTO userRequest)
		{
			var user = _userRepository.FindByEmail(userRequest.Email);
			if (user == null || !BCryptNet.Verify(userRequest.Password, user.PasswordHash))
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
