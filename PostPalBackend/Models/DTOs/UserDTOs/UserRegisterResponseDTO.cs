using PostPalBackend.Models.Enums;

namespace PostPalBackend.Models.DTOs.UserDTOs
{
    public class UserRegisterResponseDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
