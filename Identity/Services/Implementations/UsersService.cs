using AutoMapper;
using Handlers.Exceptions;
using Identity.DTO.User;
using Identity.Entities;
using Identity.Repositories.Implementations;
using Identity.Repositories.Interfaces;
using Identity.Services.Interfaces;

namespace Identity.Services.Implementations
{
    public class UsersService : IUsersService
    {

        private readonly IMapper _mapper;
        private readonly IUsersRepository _usersRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IAuthService _authService;

        public UsersService(IUsersRepository usersRepository, IMapper mapper, IAuthService authService, ICountryRepository countryRepository) { 
            _usersRepository = usersRepository;
            _mapper = mapper;
            _authService = authService;
            _countryRepository = countryRepository;
        }

        public async Task<LoginResponseDTO> Login(LoginUserRequestDTO loginUser)
        {
            var usersByUsername = await _usersRepository.GetAsync(u => u.Username.Equals(loginUser.Username));
            if (usersByUsername.Count < 1)
                throw new AppException(ExceptionCodes.LoginUsernameNotFound);

            var matchingUser = usersByUsername.Where(u => u.Password.Equals(loginUser.Password)).FirstOrDefault();
            if(matchingUser == null)
                throw new AppException(ExceptionCodes.LoginWrongPassword);

            var result = _authService.GenerateToken(matchingUser);

            if (result == null)
                throw new AppException(ExceptionCodes.CorruptedToken);


            matchingUser.RefreshToken = result.RefreshToken;
            var updated = await _usersRepository.Update(matchingUser);
            if(!updated)
                throw new AppException(ExceptionCodes.DatabaseError);

            return result;
        }

        public async Task<bool> Register(RegisterUserRequestDTO registerUser)
        {
            var matchingEmail = await _usersRepository.CountAsync(u => u.Email.Equals(registerUser.Email));
            if (matchingEmail > 0)
                throw new AppException(ExceptionCodes.RegisterEmailFound);

            var matchingUsername = await _usersRepository.CountAsync(u => u.Username.Equals(registerUser.Username));
            if (matchingUsername > 0)
                throw new AppException(ExceptionCodes.RegisterUsernameFound);

            var country = await _countryRepository.GetByShortcut(registerUser.CountryShortcut);
            if(country == null)
                throw new AppException(ExceptionCodes.CountryCodeNotFound);

            var newUser = new User()
            {
                Username = registerUser.Username,
                Password = registerUser.Password,
                Email = registerUser.Email,
                Gender = registerUser.Gender,
                Country = country
            };

            if (!await _usersRepository.Add(newUser))
                throw new AppException(ExceptionCodes.DatabaseError);

            ///send mail with activation code

            return true;
        }

        public async Task<LoginResponseDTO> RefreshToken(RefreshTokenRequestDTO refreshToken)
        {
            var newToken = await _authService.RefreshTokenAsync(refreshToken.RefreshToken, refreshToken.UserId.ToString());
            if(newToken == null)
                throw new AppException(ExceptionCodes.CorruptedToken);

            return newToken;
        }
    }
}
