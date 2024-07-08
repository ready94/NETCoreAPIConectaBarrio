using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        public ILoginService _loginSvc;

        public LoginController(ILoginService loginSvc)
        {
            _loginSvc = loginSvc;
        }

        [HttpPost("login")]
        public ActionResult<ResponseResult<string>> Login([FromBody] LoginModel login)
        {
            ResponseResult<string> res = new ResponseResult<string>(false, null, "ERROR.ERROR");
            try
            {
                //ResponseResult<string> res = new ResponseResult<string>(false, null, "LOGING.ERROR.NOT_AUTHENTICATED");
                //ResponseResult<LoginResponse> resLogin = this._loginSvc.AuthenticateUser(login);
                res = this._loginSvc.Login(login);
             

            }
            catch (Exception exc)
            {

            }
            return res;
        }

        [HttpPost("forgotPassword")]
        public ActionResult<bool> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            return Ok(this._loginSvc.ForgotPassword(forgotPassword));
        }

    }
}
