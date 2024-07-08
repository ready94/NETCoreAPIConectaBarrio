using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IUserService
    {
        bool BlockUser(int idUser);
        ResponseResult<bool> CreateUser(NewUserModel user);
        List<UserModel> GetAllUsers();
        UserModel? GetUserData(int idUser);
        bool UnblockUser(int idUser);
        bool UpdateUser(UserModel user);
        EnumRoles? GetUserRole(int idUser);
        UserDTO GetUserInfo(LoginModel login);
    }
}
