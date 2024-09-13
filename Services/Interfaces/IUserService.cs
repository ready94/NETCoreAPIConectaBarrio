using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IUserService
    {
        bool BlockUser(UserModel user, int idAdmin);
        ResponseResult<bool> CreateUser(NewUserModel user, int idAdmin = 0);
        List<UserModel> GetAllUsers();
        UserModel? GetUserData(int idUser);
        bool UnblockUser(UserModel user, int idAdmin);
        bool UpdateUser(UserModel user, int idAdmin, int idUser);
        EnumRoles? GetUserRole(int idUser);
        UserDTO GetUserInfo(LoginModel login);
        List<UserRolesDTO> GetAllUserRoles();
        bool DeleteUser(UserModel user, int idAdmin);
    }
}
