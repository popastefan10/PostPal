using PostPalBackend.Models;
using PostPalBackend.Models.DTOs.UserDTO;

namespace PostPalBackend.Services.UserService
{
	public interface IUserService
	{
		UserAuthResponseDTO? Authenticate(UserAuthRequestDTO userRequest);
		User? GetById(Guid id);
		List<User> GetAllUsers();
	}
}
