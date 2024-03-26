using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IUserService
    {
        bool BlockUser(int idUser);
        bool CreateUser(UserDTO user);
        List<UserModel> GetAllUsers();
        UserModel GetUserData(int idUser);
        bool UnblockUser(int idUser);
        bool UpdateUser(UserModel user);
        EnumRoles GetUserRole(int idUser);
    }
}
