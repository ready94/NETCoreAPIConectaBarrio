using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IUserService
    {
        bool BlockUser(int idAdmin, int idUser);
        ResponseResult<bool> CreateUser(NewUserModel user);
        List<UserModel> GetAllUsers();
        UserModel? GetUserData(int idUser);
        bool UnblockUser(int idAdmin, int idUser);
        bool UpdateUser(UserModel user, int idUserUpdate);
        EnumRoles? GetUserRole(int idUser);
        UserDTO GetUserInfo(LoginModel login);
    }
}
