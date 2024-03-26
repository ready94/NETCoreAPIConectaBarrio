namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ILoginService
    {
        bool Login(string email, string password);
        bool ForgotPassword(string email, string password);
        
    }
}
