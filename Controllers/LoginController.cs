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
        public ActionResult<ResponseResult<LoginDTO>> Login([FromBody] LoginModel login)
        {
            ResponseResult<LoginDTO> res = new ResponseResult<LoginDTO>(false, null, "ERROR.ERROR");
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

        [HttpPost("changePassword")]
        public ActionResult<ResponseResult<bool>> ForgotPassword([FromBody] ForgotPassword forgotPassword)
        {
            return Ok(this._loginSvc.ForgotPassword(forgotPassword));
        }

    }
}
