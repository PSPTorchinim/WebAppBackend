using Identity.DTO.User;
using Identity.Entities;

namespace Identity.Services.Interfaces
{
    public interface IUsersService
    {
        Task<LoginResponseDTO> Login(LoginUserRequestDTO loginUser);
        Task<bool> Register(RegisterUserRequestDTO registerUser);
        Task<LoginResponseDTO> RefreshToken(RefreshTokenRequestDTO refreshToken);
    }
}
