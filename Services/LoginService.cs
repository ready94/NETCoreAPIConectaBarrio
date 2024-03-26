using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Services.Interfaces;
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
            DataTable dt = SQLConnectionHelper.GetResultTable("SYS_T_USERS", ["EMAIL"], [email], [SQLRelationType.EQUAL]);
            
            if(dt != null &&  dt.Rows.Count > 0)
                loginCorrect = true;
            
            return loginCorrect;
        }
    }
}
