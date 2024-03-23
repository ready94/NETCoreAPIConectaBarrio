using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SQLTEST : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<string[]> TEST()
        {
            return SQLConnectionHelper.TEST();
        }
    }
}
