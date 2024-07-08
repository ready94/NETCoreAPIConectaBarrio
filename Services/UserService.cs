using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class UserService : IUserService
    {
        private const string TABLE = "SYS_T_USERS";
        public bool BlockUser(int idUser)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED"], [true], ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);
        }

        public ResponseResult<bool> CreateUser(NewUserModel user)
        {
            ResponseResult<bool> res = new(false, false, "");
            if (user != null)
            {
                try
                {
                    string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "CREATION_DATE"];
                    object[] values = [(int)EnumRoles.USER, user.Name, user.Surname, user.Username, user.Password, user.Email, DateTime.Now];

                    //1 - Check username

                    DataRow? exist = SQLConnectionHelper.GetResult("VIEW_USERS", ["USERNAME"], [user.Username], [SQLRelationType.EQUAL]);
                    if(exist != null) {
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
                    bool insert = SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
                    if (insert) {
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

                }catch(Exception exc)
                {
                    throw new Exception($"Error en CreateUser() => " + exc.Message, exc);
                }
            }
            return res;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new();
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE);
            foreach (DataRow row in dt.Rows)
            {
                users.Add(new UserModel(row));
            }
            return users;
        }

        public UserModel? GetUserData(int idUser)
        {
            DataRow? row = SQLConnectionHelper.GetResult(TABLE, ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);

            return new UserModel(row);
        }

        public bool UnblockUser(int idUser)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED"], [false], ["IDUSER"], [idUser], [SQLRelationType.EQUAL]); ;
        }

        public bool UpdateUser(UserModel user)
        {
            bool res = false;

            if (user != null)
            {
                string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "MODIFICATION_USER", "MODIFICATION_DATE", "ACTIVE", "IS_BLOCKED"];
                object[] values = [user.IdRole, user.Name, user.Surname, user.Username, user.Password, user.Email, user.ModificationUser, DateTime.Now, user.Active, user.IsBlocked];
                string[] fieldsFilter = ["IDUSER"];
                object[] valuesFilter = [user.IdUser];
                res = SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            }
            return res;
        }

        public EnumRoles? GetUserRole(int idUser)
        {
            string[] fieldsFilter = ["IDUSER"];
            object[] valuesFilter = [idUser];

            DataRow? row = SQLConnectionHelper.GetResult(TABLE, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);

            return (EnumRoles)(row?.Field<EnumRoles>("IDROLE"));

        }

        public UserDTO? GetUserInfo(LoginModel login)
        {
            UserDTO user = null;
            try{
                List<string> fields = [];
                List<object> values = [];
                List<string> relations = [];

                if (login.UserName != null || login.UserName != string.Empty)
                {
                    fields.Add("USERNAME");
                    values.Add(login.UserName);
                    relations.Add(SQLRelationType.EQUAL);
                }
                if (login.Email != null || login.Email != string.Empty)
                {
                    fields.Add("EMAIL");
                    values.Add(login.Email);
                    relations.Add(SQLRelationType.EQUAL);
                }

                DataRow? row = SQLConnectionHelper.GetResult("VIEW_USERS", [..fields], [..values], [..relations]);
                if (row != null)
                {
                    user = new UserDTO
                    {
                        IdRole = row.Field<EnumRoles>("IDROLE"),
                        IdUser = row.Field<int>("IDUSER"),
                        Name = row.Field<string>("NAME"),
                        Surname = row.Field<string>("SURNAME"),
                        Username = row.Field<string>("USERNAME"),
                        Email = row.Field<string>("EMAIL"),
                        CultureKey = row.Field<string>("CULTURE_KEY"),
                        Password = row.Field<string>("PASSWORD")
                    };
                }
            }
            catch(Exception exc)
            {
                throw new Exception($"Error en GetUserInfo() => " + exc.Message, exc);
            }

            return user;

        }
    }
}
