using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class AdminService : IAdminService
    {
        private IUserService _userService;
        public AdminService(IUserService userSvc)
        {
            _userService = userSvc;
        }

        public List<AdminOptionModel> GetAdminOptions()
        {
            List<AdminOptionModel> options = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_M_ADMIN_OPTIONS");
                foreach (DataRow row in dt.Rows)
                {
                    options.Add(new AdminOptionModel(row));
                }

            }
            catch (Exception ex) { }
            return options;
        }

        #region Users
        public List<UserModel> GetAllUsers()
        {
            List<UserModel> options = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_T_USERS");
                foreach (DataRow row in dt.Rows)
                {
                    options.Add(new UserModel(row));
                }

            }
            catch (Exception ex) { }
            return options;
        }
        public bool UpdateUser(UserModel user) {
            bool res = false;

            if (user != null)
            {
                string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "MODIFICATION_USER", "MODIFICATION_DATE", "ACTIVE", "IS_BLOCKED"];
                object[] values = [user.IdRole, user.Name, user.Surname, user.Username, user.Password, user.Email, user.ModificationUser, DateTime.Now, user.Active, user.IsBlocked];
                string[] fieldsFilter = ["IDUSER"];
                object[] valuesFilter = [user.IdUser];
                res = SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", fields, values, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            }
            return res;
        }
        public bool DeleteUser(UserModel user) {

            return SQLConnectionHelper.DeleteBBDD("SYS_T_USERS", ["IDUSER"], [user.IdUser], [SQLRelationType.EQUAL]);
        }
        public ResponseResult<bool> CreateUser(NewUserModel user)
        {
            ResponseResult<bool> res = new ResponseResult<bool>();
            if (user != null)
            {
                try
                {
                    string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "CREATION_DATE"];
                    object[] values = [(int)EnumRoles.USER, user.Name, user.Surname, user.Username, user.Password, user.Email, DateTime.Now];

                    //1 - Check username

                    DataRow? exist = SQLConnectionHelper.GetResult("VIEW_USERS", ["USERNAME"], [user.Username], [SQLRelationType.EQUAL]);
                    if (exist != null)
                    {
                        res.Result = false;
                        res.Msg = "USER.ALREADY_EXISTS_USERNAME";
                        res.Success = false;
                        return res;
                    }

                    //2 - Check email
                    exist = SQLConnectionHelper.GetResult("VIEW_USERS", ["EMAIL"], [user.Email], [SQLRelationType.EQUAL]);
                    if (exist != null)
                    {
                        res.Result = false;
                        res.Msg = "USER.ALREADY_EXISTS_EMAIL";
                        res.Success = false;
                        return res;
                    }

                    //3 - Insert
                    bool insert = SQLConnectionHelper.InsertBBDD("SYS_T_USERS", fields, values);
                    if (insert)
                    {
                        res.Result = true;
                        res.Msg = "USER.CREATED_SUCCESS";
                        res.Success = true;
                        return res;
                    }
                    else
                    {
                        res.Result = false;
                        res.Msg = "USER.CREATED_ERROR";
                        res.Success = false;
                        return res;
                    }

                }
                catch (Exception exc)
                {
                    throw new Exception($"Error en CreateUser() => " + exc.Message, exc);
                }
            }
            return res;
        }
        public bool BlockUser(int idUser) {
            return SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["IS_BLOCKED"], [true], ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);
        }
        public bool UnblockUser(int idUser) {

            return SQLConnectionHelper.UpdateBBDD("SYS_T_USERS", ["IS_BLOCKED"], [false], ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);
        }
        #endregion

        #region News
        public List<NewsModel> GetNews()
        {
            List<NewsModel> options = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_T_NEWS");
                foreach (DataRow row in dt.Rows)
                {
                    options.Add(new NewsModel(row));
                }

            }
            catch (Exception ex) { }
            return options;
        }
        public bool UpdateNew(NewsModel news, int idUser)
        {
            string[] fields = ["IDCATEGORY", "NAME", "DESCRIPTION", "MODIFICATION_USER", "MODIFICATION_DATE", "START_DATE", "END_DATE", "ACTIVE"];
            object[] values = [news.IdCategory, news.Name, news.Description, idUser, DateTime.Now, news.StartDate, news.EndDate, news.Active];
            return SQLConnectionHelper.UpdateBBDD("SYS_T_NEWS", fields, values, ["IDNEW"], [news.IdNew], [SQLRelationType.EQUAL]);
        }

        public bool CreateNew(NewsModel news, int idUser)
        {
            string[] fields = ["IDCATEGORY", "NAME", "DESCRIPTION", "CREATION_USER", "CREATION_DATE", "START_DATE", "END_DATE", "ACTIVE"];
            object[] values = [(int)news.IdCategory, news.Name, news.Description, idUser, DateTime.Now, news.StartDate, news.EndDate, true];
            return SQLConnectionHelper.InsertBBDD("SYS_T_NEWS", fields, values);
        }

        public bool DeleteNew(int idNew, int idUser)
        {
            if (this._userService.GetUserRole(idUser) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD("SYS_T_NEWS", ["IDNEW"], [idNew], [SQLRelationType.EQUAL]);
            else
                return SQLConnectionHelper.UpdateBBDD("SYS_T_NEWS", ["ACTIVE"], [false], ["IDNEW"], [idNew], [SQLRelationType.EQUAL]);
        }
        #endregion

        #region Complaints
        public List<ComplaintModel> GetComplaints()
        {
            List<ComplaintModel> options = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_T_COMPLAINTS");
                foreach (DataRow row in dt.Rows)
                {
                    options.Add(new ComplaintModel(row));
                }

            }
            catch (Exception ex) { }
            return options;
        }

        public bool UpdateComplaint(ComplaintModel complaint, int idUser)
        {
            if (complaint != null)
            {
                string[] fields = ["IDCOMPLAINT_TYPE", "IDPRIORITY", "COMPLAINT_TITLE", "COMPLAINT_DESCRIPTION", "MODIFICATION_USER", "MODIFICATION_DATE", "ACTIVE"];
                object[] values = [complaint.IdComplaintType, complaint.IdPriority, complaint.Title, complaint.Description, idUser, DateTime.Now, complaint.Active];
                string[] fieldsFilter = ["IDCOMPLAINT"];
                object[] valuesFilter = [complaint.IdComplaint];

                return SQLConnectionHelper.UpdateBBDD("SYS_T_COMPLAINTS", fields, values, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            }
            else return false;
        }
        public bool CreateComplaint(ComplaintDTO complaint, int idUser)
        {
            bool res = false;

            if (complaint != null)
            {
                string[] fields = ["IDCOMPLAINT_TYPE", "IDPRIORITY", "COMPLAINT_TITLE", "COMPLAINT_DESCRIPTION", "CREATION_USER", "CREATION_DATE", "ACTIVE"];
                object[] values = [(int)complaint.IdComplaintType, (int)complaint.IdPriority, complaint.Title, complaint.Description, idUser, DateTime.Now, true];
                res = SQLConnectionHelper.InsertBBDD("SYS_T_COMPLAINTS", fields, values);
            }

            return res;
        }

        public bool DeleteComplaint(int idUser, int idComplaint)
        {
            // Si el usuario es admin, se hace un borrado fisico, si no, un borrado logico
            if (this._userService.GetUserRole(idUser) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD("SYS_T_COMPLAINTS", ["IDCOMPLAINT_TYPE"], [idComplaint], [SQLRelationType.EQUAL]);
            else
                return SQLConnectionHelper.UpdateBBDD("SYS_T_COMPLAINTS", ["ACTIVE"], [false], ["IDCOMPLAINT_TYPE"], [idComplaint], [SQLRelationType.EQUAL]);
        }
        #endregion

        #region Events
        public List<ActivityModel> GetActivities()
        {
            List<ActivityModel> options = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("SYS_T_EVENTS");
                foreach (DataRow row in dt.Rows)
                {
                    options.Add(new ActivityModel(row));
                }

            }
            catch (Exception ex) { }
            return options;
        }
        public bool UpdateActivity(ActivityModel activity, int idUser)
        {
            string[] fields = ["IDEVENT_TYPE", "IDEVENT_SUBCATEGORY", "LOCATION", "MAX_PERSON", "ACTUAL_PERSON", "EVENT_DATE"];
            object[] values = [activity.IdEventType, activity.IdEventSubCategory, activity.Location, activity.MaxPerson,
                                activity.ActualPerson, activity.EventDate];

            bool res = SQLConnectionHelper.UpdateBBDD("SYS_T_EVENTS", fields, values, ["IDEVENT"], [activity.IdEvent], [SQLConnectionHelper.EQUAL]);

            return res;
        }
        public bool DeleteActivity(int idactivity, int idUser) {
            {
                if (this._userService.GetUserRole(idUser) == EnumRoles.ADMIN)
                    return SQLConnectionHelper.DeleteBBDD("SYS_T_EVENTS", ["IDEVENT"], [idactivity], [SQLRelationType.EQUAL]);
                else
                    return false;

            }
        }
        public bool CreateActivity(ActivityModel activity)
        {
            string[] fields = ["IDEVENT_TYPE", "IDEVENT_SUBCATEGORY", "LOCATION", "MAX_PERSON", "ACTUAL_PERSON", "CREATION_USER", "EVENT_DATE", "CREATION_DATE"];
            object[] values = [activity.IdEventType, activity.IdEventSubCategory, activity.Location, activity.MaxPerson,
                                1, activity.CreationUser, activity.EventDate, DateTime.Now];

            int res = SQLConnectionHelper.InsertIdentityBBDD("SYS_T_EVENTS", fields, values);

            SQLConnectionHelper.InsertBBDD("sys_rel_user_event", ["IDUSER", "IDEVENT"], [activity.CreationUser, res]);

            return true;
        }
        #endregion



    }
}
