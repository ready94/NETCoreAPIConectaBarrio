using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.Models
{
    public class ComplaintModel
    {
        public int IdComplaint { get; set; }
        public EnumComplaintTypes IdComplaintType { get; set; }
        public EnumComplaintPriority IdPriority { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModificationUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public bool Active { get; set; }

        public ComplaintModel() { }

        public ComplaintModel(MySqlDataReader dr)
        {
            IdComplaint = dr.GetInt32("IDCOMPLAINT");
            IdComplaintType = (EnumComplaintTypes)dr.GetInt32("IDCOMPLAINT_TYPE");
            IdPriority = (EnumComplaintPriority)dr.GetInt32("IDPRIORITY");
            Title = dr.GetString("COMPLAINT_TITLE");
            Description = dr.GetString("COMPLAINT_DESCRIPTION");
            CreationUser = dr.GetInt32("CREATION_USER");
            CreationDate = dr.GetDateTime("CREATION_DATE");
            ModificationUser = dr.GetInt32("MODIFICATION_USER");
            ModificationDate = dr.GetDateTime("MODIFICATION_DATE");
            Active = dr.GetBoolean("ACTIVE");
        }
    }
}
