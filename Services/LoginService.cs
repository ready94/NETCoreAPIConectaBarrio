using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class LoginService : ILoginService
    {
        public bool ForgotPassword(string email, string password)
        {
            return SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["PASSWORD"], [password], ["EMAIL"], [email]);
        }

        public bool Login(string email, string password)
        {
            bool loginCorrect = false;
            DataRow? row = SQLConnectionHelper.GetResult("SYS_T_USERS", ["EMAIL"], [email], [SQLRelationType.EQUAL]);
            
            loginCorrect = row != null;
            
            return loginCorrect;
        }
    }
}
