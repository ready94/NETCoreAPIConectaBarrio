using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.DTOs
{
    public class UserRolesDTO
    {
        public EnumRoles IdRole { get; set; }
        public string TranslationKey { get; set; }

        public UserRolesDTO(DataRow row)
        {
            IdRole = (EnumRoles)row.Field<int>("IDROLE");
            TranslationKey = row.Field<string>("TRANSLATION_KEY");
        }
    }
}
