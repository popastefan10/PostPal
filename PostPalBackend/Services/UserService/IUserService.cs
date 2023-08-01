using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;

namespace PostPalBackend.Services.UserService
{
	public interface IUserService
	{
		UserRegisterResponseDTO? Register(UserRegisterRequestDTO requestDTO);
		UserAuthResponseDTO? Authenticate(UserAuthRequestDTO requestDTO);
		User? GetById(Guid id);
		List<User> GetAllUsers();
	}
}
