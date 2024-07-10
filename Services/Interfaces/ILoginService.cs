using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ILoginService
    {
        ResponseResult<LoginDTO> Login(LoginModel login);
        ResponseResult<bool> ForgotPassword(ForgotPassword forgotPassword);
        ResponseResult<LoginResponse> AuthenticateUser(LoginModel login);
    }
}
