using Handlers.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Handlers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BaseController: ControllerBase
    {

        public async Task<IActionResult> Handle<T>(Func<Task<T>> action, ILogger logger)
        {
            try
            {
                return Ok(await ExceptionHandler.Handle(async () => await action(), logger));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
