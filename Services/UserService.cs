﻿using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Enums;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class UserService : IUserService
    {
        private const string TABLE = "SYS_T_USERS";
        public bool BlockUser(int idUser)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED"], [true], ["IDUSER"], [idUser]);
        }

        public bool CreateUser(UserDTO user)
        {
            bool res = false;
            if (user != null)
            {
                string[] fields = ["IDROLE", "NAME", "SURNAME", "USERNAME", "PASSWORD", "EMAIL", "CREATION_USER", "CREATION_DATE"];
                object[] values = [user.IdRole, user.Name, user.Surname, user.Username, user.Password, user.Email, user.CreationUser, DateTime.Now];
                res = SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
            }

            return res;
        }

        public List<UserModel> GetAllUsers()
        {
            List<UserModel> users = new();
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE, [], [], []);
            foreach (DataRow row in dt.Rows)
            {
                users.Add(new UserModel(row));
            }
            return users;
        }

        public UserModel GetUserData(int idUser)
        {
            UserModel user = null;
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE, ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);
            if (dt != null && dt.Rows.Count > 0)
            {
                user = new UserModel(dt.Rows[0]);
            }
            return user;
        }

        public bool UnblockUser(int idUser)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["IS_BLOCKED"], [false], ["IDUSER"], [idUser]); ;
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
                res = SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, fieldsFilter, valuesFilter);
            }
            return res;
        }

        public EnumRoles GetUserRole(int idUser)
        {
            string[] fieldsFilter = ["IDUSER"];
            object[] valuesFilter = [idUser];
            UserModel user = null;

            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE, fieldsFilter, valuesFilter, [SQLRelationType.EQUAL]);
            if (dt != null && dt.Rows.Count > 0)
                user = new(dt.Rows[0]);

            return user.IdRole;

        }
    }
}
