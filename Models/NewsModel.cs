using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.Models
{
    public class NewsModel
    {
        public int IdNew { get; set; }
        public EnumNewsCategory IdCategory { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModificationUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public NewsModel() { }

        public NewsModel(MySqlDataReader dr)
        {
            IdNew = dr.GetInt32("IDNEW");
            IdCategory = (EnumNewsCategory)dr.GetInt32("IDCATEGORY");
            Name = dr.GetString("NAME");
            Description = dr.GetString("DESCRITPION");
            CreationUser = dr.GetInt32("CREATION_USER");
            CreationDate = dr.GetDateTime("CREATION_DATE");
            ModificationUser = dr.GetInt32("MODIFICATION_USER");
            ModificationDate = dr.GetDateTime("MODIFICATION_DATE");
            StartDate = dr.GetDateTime("START_DATE");
            EndDate = dr.GetDateTime("END_DATE");
            Active = dr.GetBoolean("ACTIVE");
        }
    }
}
