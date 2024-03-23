using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("login")]
        public ActionResult<bool> Login([FromBody] LoginRequestModel login)
        {
          return Ok(true);
        }

        [HttpPost("forgotPassword")]
        public ActionResult<bool> ForgotPassword([FromBody] LoginRequestModel login)
        {
            return Ok(true);
        }

    }
}
