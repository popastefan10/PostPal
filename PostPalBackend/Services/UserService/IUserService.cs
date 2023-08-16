using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;
using PostPalBackend.Models.DTOs.UserDTOs;

namespace PostPalBackend.Services.UserService
{
	public interface IUserService
	{
		UserRegisterResponseDTO? Register(UserRegisterRequestDTO requestDTO);
		UserAuthResponseDTO? Authenticate(UserAuthRequestDTO requestDTO);
		User Ban(User user);
		User RemoveBan(User user);
		User? GetById(Guid id);
		List<User> GetAllUsers();
		User Update(Guid id, UserUpdateDTO dto);
		User Delete(Guid id);
	}
}
