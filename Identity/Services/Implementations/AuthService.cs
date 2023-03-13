using Handlers.Exceptions;
using Identity.DTO.User;
using Identity.Entities;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace TherapyAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersRepository _usersRepository;

        public AuthService(IConfiguration configuration, IUsersRepository usersRepository, ILogger<AuthService> logger)
        {
            _configuration = configuration;
            _usersRepository = usersRepository;
        }

        private List<Claim> GenerateClaims(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email)
            };
            user.Roles.ForEach(r => {
                r.Permissions.ForEach(p =>
                {
                    claims.Add(new Claim(ClaimTypes.Role, p.Name));
                });
            });
            return claims;
        }

        private string GenerateRefresh()
        {
            var random = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }

        public LoginResponseDTO GenerateToken(User user)
        {
            var key = _configuration.GetSection("TokenConfiguration").GetValue<string>("Key");
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var time = _configuration.GetSection("TokenConfiguration").GetValue<long>("TokenExpiration");
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddSeconds(time),
                claims: GenerateClaims(user),
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
            );
            var tokenResponse = new LoginResponseDTO();
            tokenResponse.Token = new JwtSecurityTokenHandler().WriteToken(token);
            tokenResponse.RefreshToken = GenerateRefresh();
            tokenResponse.UserId = user.Id;
            return tokenResponse;
        }

        public async Task<LoginResponseDTO> RefreshTokenAsync(string token, string UserId)
        {
            var readedToken = new JwtSecurityTokenHandler().ReadToken(token) as JwtSecurityToken;
            var userId = readedToken?.Claims.FirstOrDefault(p => p.Type.Equals("id"));

            if (userId == null)
                throw new AppException(ExceptionCodes.CorruptedToken);

            if(!userId.Equals(UserId))
                throw new AppException(ExceptionCodes.CorruptedToken);

            var user = await _usersRepository.GetAsync(new Guid(userId.Value));

            if (user == null)
                throw new AppException(ExceptionCodes.CorruptedToken);

            return GenerateToken(user);
        }

        public bool ValidateToken(string authToken, bool isInvited = false)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = _configuration.GetSection("TokenConfiguration").GetValue<string>("Key");
                var encodedKey = Encoding.ASCII.GetBytes(key);
                var time = _configuration.GetSection("TokenConfiguration").GetValue<long>("TokenExpiration");
                tokenHandler.ValidateToken(authToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(encodedKey),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.FromSeconds(time)
                }, out SecurityToken validatedToken);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
