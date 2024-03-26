using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class NewsModel
    {
        public int IdNew { get; set; }
        public EnumNewsCategory IdCategory { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool Active { get; set; }

        public NewsModel() { }

        public NewsModel(DataRow row)
        {
            IdNew = row.Field<int>("IDNEW");
            IdCategory = (EnumNewsCategory)row.Field<int>("IDCATEGORY");
            Name = row.Field<string?>("NAME");
            Description = row.Field<string?>("DESCRITPION");
            CreationUser = row.Field<int>("CREATION_USER");
            CreationDate = row.Field<DateTime>("CREATION_DATE");
            ModificationUser = row.Field<int?>("MODIFICATION_USER");
            ModificationDate = row.Field<DateTime?>("MODIFICATION_DATE");
            StartDate = row.Field<DateTime>("START_DATE");
            EndDate = row.Field<DateTime>("END_DATE");
            Active = row.Field<bool>("ACTIVE");
        }
    }
}
