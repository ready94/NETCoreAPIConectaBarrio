using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class ComplaintModel
    {
        public int IdComplaint { get; set; }
        public EnumComplaintTypes IdComplaintType { get; set; }
        public EnumComplaintPriority IdPriority { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public bool Active { get; set; }

        public ComplaintModel() { }

        public ComplaintModel(DataRow row)
        {
            IdComplaint = row.Field<int>("IDCOMPLAINT");
            IdComplaintType = (EnumComplaintTypes)row.Field<int>("IDCOMPLAINT_TYPE");
            IdPriority = (EnumComplaintPriority)row.Field<int>("IDPRIORITY");
            Title = row.Field<string?>("COMPLAINT_TITLE");
            Description = row.Field<string?>("COMPLAINT_DESCRIPTION");
            CreationUser = row.Field<int>("CREATION_USER");
            CreationDate = row.Field<DateTime>("CREATION_DATE");
            ModificationUser = row.Field<int?>("MODIFICATION_USER");
            ModificationDate = row.Field<DateTime?>("MODIFICATION_DATE");
            Active = row.Field<bool>("ACTIVE");
        }
    }
}
