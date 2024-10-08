﻿using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;
using System.Data.Entity;

namespace NETCoreAPIConectaBarrio.Services
{
    public class UserService : IUserService
    {
        private const string TABLE = "SYS_T_USERS";
    

        public ResponseResult<bool> CreateUser(NewUserModel user, int idAdmin)
        {
            ResponseResult<bool> res = new(false, false, "");
            if (user != null)
            {
                try
                {
                    if (user.IdRole == null) 
                        user.IdRole = EnumRoles.USER;

                    string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "CREATION_DATE", "CREATION_USER"];
                    object[] values = [(int)user.IdRole, user.Name, user.Surname, user.Username, user.Password, user.Email, DateTime.Now, idAdmin];

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
                    bool insert = SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
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

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = [];
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
            UserModel? user = (row != null) ? new UserModel(row) : null;
            return user;
        }

        public bool UnblockUser(UserModel user, int idAdmin)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED", "MODIFICATION_DATE", "MODIFICATION_USER"], [0, DateTime.Now, idAdmin], ["IDUSER"], [user.IdUser], [SQLRelationType.EQUAL]);
        }
        public bool BlockUser(UserModel user, int idAdmin)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED", "MODIFICATION_DATE", "MODIFICATION_USER"], [1, DateTime.Now, idAdmin], ["IDUSER"], [user.IdUser], [SQLRelationType.EQUAL]);
        }

        public bool UpdateUser(UserModel user, int idAdmin, int idUser)
        {
            bool res = false;

            if (user != null)
            {
                string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "MODIFICATION_USER", "MODIFICATION_DATE", "ACTIVE", "IS_BLOCKED"];
                object[] values = [(int)user.IdRole, user.Name, user.Surname, user.Username, user.Password, user.Email, idAdmin, DateTime.Now, user.Active, user.IsBlocked];
                string[] fieldsFilter = ["IDUSER"];
                object[] valuesFilter = [idUser];
                res = SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            }
            return res;
        }

        public EnumRoles? GetUserRole(int idUser)
        {
            string[] fieldsFilter = ["IDUSER"];
            object[] valuesFilter = [idUser];

            DataRow? row = SQLConnectionHelper.GetResult(TABLE, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            EnumRoles? idRole = row?.Field<EnumRoles>("IDROLE") ?? null;
            return idRole;

        }

        public UserDTO? GetUserInfo(LoginModel login)
        {
            UserDTO user = null;
            try
            {
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

                DataRow? row = SQLConnectionHelper.GetResult("VIEW_USERS", [.. fields], [.. values], [.. relations]);
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
            catch (Exception exc)
            {
                throw new Exception($"Error en GetUserInfo() => " + exc.Message, exc);
            }

            return user;

        }

        public List<UserRolesDTO> GetAllUserRoles()
        {
            List<UserRolesDTO> roles = [];
            DataTable dt = SQLConnectionHelper.GetResultTable("SYS_M_ROLE");
            foreach (DataRow row in dt.Rows)
            {
                roles.Add(new UserRolesDTO(row));
            }
            return roles;
        }

        public bool DeleteUser(UserModel user, int idAdmin)
        {
            if (this.GetUserRole(idAdmin) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD(TABLE, ["IDUSER"], [user.IdUser], [SQLRelationType.EQUAL]);
            else
               return false;
        }
    }
}
