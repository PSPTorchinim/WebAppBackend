using Identity.DTO.User;
using Identity.Entities;

namespace Identity.Services.Interfaces
{
    public interface IAuthService
    {
        LoginResponseDTO GenerateToken(User user);
        Task<LoginResponseDTO> RefreshTokenAsync(string token, string UserId);
        bool ValidateToken(string authToken, bool isInvited = false);
    }
}