using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class OptionsModel
    {
        public int IdOption { get; set; }
        public string Value { get; set; }
        public string Icon { get; set; }
        public bool OnlyAdmin { get; set; }

        public OptionsModel(DataRow row) {
            IdOption = row.Field<int>("IDOPTION");
            Value = row.Field<string>("VALUE");
            Icon = row.Field<string>("ICON");
            OnlyAdmin = row.Field<bool>("ONLY_ADMIN");
        }
    }
}
