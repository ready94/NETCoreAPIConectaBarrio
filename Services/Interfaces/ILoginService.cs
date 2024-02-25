namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ILoginService
    {
        bool Login(string email, string password);
        bool ChangePassword(string email);
        
    }
}
