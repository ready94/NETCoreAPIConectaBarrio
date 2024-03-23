using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IUserService
    {
        bool BlockUser(object user);
        bool CreateUser(UserModel user);
        List<UserDTO> GetAllUsers();
        UserDTO GetUserData(object user);
        bool UnblockUser(object user);
        bool UpdateUser(UserModel user);
    }
}
