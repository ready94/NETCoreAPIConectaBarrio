using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public EnumRoles IdRole { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModificationUser { get; set; }
        public DateTime ModificationDate { get; set;}
        public bool Active { get; set; }
        
        public UserModel() { }

        public UserModel(MySqlDataReader dr)
        {
            IdUser = dr.GetInt32("IDUSER");
            IdRole = (EnumRoles)dr.GetInt32("IDROLE");
            Name = dr.GetString("NAME");
            Surname = dr.GetString("SURNAME");
            Username = dr.GetString("USERNAME");
            Password = dr.GetString("PASSWORD");
            Email = dr.GetString("EMAIL");
            CreationUser = dr.GetInt32("CREATION_USER");
            CreationDate = dr.GetDateTime("CREATION_DATE");
            ModificationUser = dr.GetInt32("MODIFICATION_USER");
            ModificationDate = dr.GetDateTime("MODIFICATION_DATE");
            Active = dr.GetBoolean("ACTIVE");
        }
    }
}
