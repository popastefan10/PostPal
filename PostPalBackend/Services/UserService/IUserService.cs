using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;

namespace PostPalBackend.Services.UserService
{
	public interface IUserService
	{
		UserResponseDTO? Authenticate(UserRequestDTO userRequest);
		User? GetById(Guid id);
	}
}
