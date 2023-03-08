using Microsoft.AspNetCore.Mvc;
using System.Net;
using Handlers;

namespace Identity.Controllers
{
    [ApiController]
    public class UsersController : BaseController
    {
        public UsersController() {
        
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
    }
}
