using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class AdminOptionModel
    {
        public EnumAdminOptions IdOption { get; set; }
        public string Value {  get; set; }
        
        public AdminOptionModel(DataRow row)
        {
            IdOption = row.Field<EnumAdminOptions>("IDOPTION");
            Value = row.Field<string>("VALUE");
        }
    }
}
