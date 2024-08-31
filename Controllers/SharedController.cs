using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SharedController : ControllerBase
    {
        private ISharedService _sharedSvc;
        public SharedController(ISharedService sharedSvc) {
            _sharedSvc = sharedSvc;
        }
            
        [HttpGet("getMenuOptions")]
        public ActionResult<List<OptionsModel>> GetAllComplaints()
        {
            return Ok(_sharedSvc.GetMenuOptions());
        }


    }
}
