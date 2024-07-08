using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ILoginService
    {
        ResponseResult<string> Login(LoginModel login);
        bool ForgotPassword(ForgotPassword forgotPassword);
        ResponseResult<LoginResponse> AuthenticateUser(LoginModel login);
    }
}
