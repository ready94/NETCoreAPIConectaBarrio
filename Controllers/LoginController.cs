using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [HttpPost("")]
        public ActionResult<bool> Login([FromBody] LoginReq login)
        {
          return Ok(true);
        }

    }
}
