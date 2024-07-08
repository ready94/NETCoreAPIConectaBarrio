namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ISessionService
    {
        Guid CreateSession(int idUser, string IP);
        bool RemoveSession(int idUser);

    }
}
