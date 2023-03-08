using Microsoft.AspNetCore.Mvc;
using System.Net;
using Handlers;
using Identity.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Identity.DTO.User;

namespace Identity.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        private readonly IUsersService _usersService;
        private readonly ILogger _logger;

        public UsersController(IUsersService usersService, ILogger logger)
        {
            _usersService = usersService;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserRequestDTO loginUser)
        {
            return await Handle(async () => await _usersService.Login(loginUser), _logger);
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(LoginUserRequestDTO loginUser)
        {
            return await Handle(async () => await _usersService.Login(loginUser), _logger);
        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDTO refreshToken)
        {
            return await Handle(async () => await _usersService.RefreshToken(refreshToken), _logger);
        }
    }
}
