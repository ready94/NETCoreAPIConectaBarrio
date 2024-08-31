using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IAdminService
    {
        List<AdminOptionModel> GetAdminOptions();

        #region Users
        List<UserModel> GetAllUsers();
        bool UpdateUser(UserModel user);
        bool DeleteUser(UserModel user);
        ResponseResult<bool> CreateUser(NewUserModel user);
        bool BlockUser(int idUser);
        bool UnblockUser(int idUser);
        #endregion

        #region News
        List<NewsModel> GetNews();
        bool UpdateNew(NewsModel news, int idUser);
        bool DeleteNew(int idNew, int idUser);
        bool CreateNew(NewsModel news, int idUser);
        #endregion

        #region Complaints
        List<ComplaintModel> GetComplaints();
        bool UpdateComplaint(ComplaintModel complaint, int idUser);
        bool DeleteComplaint(int idUser, int idComplaint);
        bool CreateComplaint(ComplaintDTO complaint, int idUser);
        #endregion

        #region Events
        List<ActivityModel> GetActivities();
        bool UpdateActivity(ActivityModel activity, int idUser);
        bool DeleteActivity(int idactivity, int idUser);
        bool CreateActivity(ActivityModel activity);
        #endregion


    }
}
