using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.DTOs;

namespace NETCoreAPIConectaBarrio.Services
{
    public class LoginService : ILoginService
    {
        private IUserService _userService;


        public LoginService(IUserService userService)
        {
            _userService = userService;
        }

        public ResponseResult<bool> ForgotPassword(ForgotPassword forgotPassword)
        {
            ResponseResult<bool> res = new ResponseResult<bool>(false, false, "");
            if (forgotPassword != null)
            {
                res.Result = SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["PASSWORD"], [forgotPassword.NewPassword], ["EMAIL", "USERNAME"],
                    [forgotPassword.email, forgotPassword.Username], [SQLConnectionHelper.EQUAL, SQLConnectionHelper.EQUAL]);
                if (res.Result)
                {
                    res.Success = true;
                    res.Msg = "LOGIN.FORGOT_PASSWORD.CHANGE_PASSWORD_SUCCESS";
                }

            }
            return res;
        }

        public ResponseResult<LoginDTO> Login(LoginModel login)
        {

            ResponseResult<LoginDTO> res = new ResponseResult<LoginDTO>(false, null, "ERROR.ERROR");
            List<string> fields = ["PASSWORD"];
            List<object> values = [login.Password];
            List<string> relations = [SQLRelationType.EQUAL];

            if (login.UserName != "")
            {
                fields.Add("USERNAME");
                values.Add(login.UserName);
                relations.Add(SQLRelationType.EQUAL);
            }

            if (login.Email != "")
            {
                fields.Add("EMAIL");
                values.Add(login.Email);
                relations.Add(SQLRelationType.EQUAL);
            }


            DataRow? row = SQLConnectionHelper.GetResult("SYS_T_USERS", [.. fields], [.. values], [.. relations]);
            if (row != null)
            {
                LoginDTO loginDto = new()
                {
                    IdUser = row.Field<int>("IDUSER"),
                    UserName = row.Field<string>("USERNAME"),
                    IdRole = row.Field<EnumRoles>("IDROLE")
                };

                res.Success = true;
                res.Result = loginDto;
                res.Msg = "";

                SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["IP"], [login.Ip], [.. fields], [.. values], [.. relations]);
            }
            return res;
        }

        public ResponseResult<LoginResponse> AuthenticateUser(LoginModel login)
        {
            ResponseResult<LoginResponse> response = new(false, null, "LOGIN.ERROR.INCORRECT_DATA");

            try
            {
                UserDTO user = this._userService.GetUserInfo(login);
                if (user == null)
                {
                    response.Msg = "LOGIN.ERROR.NOT_ACTIVE_OR_BLOCKED";
                }
                else
                {
                }


            }
            catch (Exception exc)
            {
                throw new Exception($"Error en AuthenticateUser() => " + exc.Message, exc);
            }
            return response;
        }

    }
}
