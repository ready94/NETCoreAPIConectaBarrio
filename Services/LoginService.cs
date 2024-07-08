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

        public bool ForgotPassword(ForgotPassword forgotPassword)
        {
            bool res = false;
            if (forgotPassword != null)
            {
                if (forgotPassword.NewPassword.Equals(forgotPassword.NewPasswordCheck))
                {
                    res = SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["PASSWORD"], [forgotPassword.NewPassword], ["EMAIL"], [forgotPassword.email], [SQLConnectionHelper.EQUAL]);
                }
            }
            return res;
        }

        public ResponseResult<string> Login(LoginModel login)
        {

            ResponseResult<string> res = new ResponseResult<string>(false, "", "ERROR.ERROR");
            List<string> fields = ["PASSWORD"];
            List<object> values = [login.Password];
            List<string> relations = [SQLRelationType.EQUAL];

            if(login.UserName != "")
                { fields.Add("USERNAME");
                values.Add(login.UserName);
                relations.Add(SQLRelationType.EQUAL);
            }

            if (login.Email != "")
            {
                fields.Add("EMAIL");
                values.Add(login.Email);
                relations.Add(SQLRelationType.EQUAL);
            }


            DataRow? row = SQLConnectionHelper.GetResult("SYS_T_USERS", [..fields], [..values], [..relations]);
            if(row != null)
            {

                res.Success = true;
                res.Result = row.Field<string>("USERNAME") ?? "";
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
                if(user == null)
                {
                    response.Msg = "LOGIN.ERROR.NOT_ACTIVE_OR_BLOCKED";
                }
                else
                {
                }


            }catch(Exception exc)
            {
                throw new Exception($"Error en AuthenticateUser() => " + exc.Message, exc);
            }
            return response;
        }

    }
}
