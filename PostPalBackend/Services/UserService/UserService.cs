using BCryptNet = BCrypt.Net.BCrypt;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Repositories.UserRepository;
using PostPalBackend.Models;
using PostPalBackend.Helpers.Jwt;

namespace PostPalBackend.Services.UserService {
	public class UserService : IUserService {
		private readonly IUserRepository _userRepository;
		private readonly IJwtUtils _jwtUtils;

		public UserService(IUserRepository userRepository, IJwtUtils jwtUtils) {
			_userRepository = userRepository;
			_jwtUtils = jwtUtils;
		}

		public UserResponseDTO? Authenticate(UserRequestDTO userRequest) {
			var user = _userRepository.FindByEmail(userRequest.Email);
			if (user == null || !BCryptNet.Verify(userRequest.Password, user.PasswordHash)) {
				return null;
			}
			var token = _jwtUtils.GenerateJwtToken(user);

			return new UserResponseDTO(user, token);
		}

		public User? GetById(Guid id) {
			return this._userRepository.FindById(id);
		}
	}
}
