using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class UserModel
    {
        public int IdUser { get; set; }
        public EnumRoles IdRole { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set;}
        public int Active { get; set; }
        public int IsBlocked { get; set; }


        public UserModel() { }

        public UserModel(DataRow row)
        {
            IdUser = row.Field<int>("IDUSER");
            IdRole = (EnumRoles)row.Field<int>("IDROLE");
            Name = row.Field<string?>("NAME");
            Surname = row.Field<string?>("SURNAME");
            Username = row.Field<string?>("USERNAME");
            Password = row.Field<string?>("PASSWORD");
            Email = row.Field<string?>("EMAIL");
            CreationUser = row.Field<int>("CREATION_USER");
            CreationDate = row.Field<DateTime>("CREATION_DATE");
            ModificationUser = row.Field<int?>("MODIFICATION_USER");
            ModificationDate = row.Field<DateTime?>("MODIFICATION_DATE");
            Active = row.Field<int>("ACTIVE");
            IsBlocked = row.Field<int>("IS_BLOCKED");
        }
    }
}
