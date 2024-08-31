using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.DTOs
{
    public class LoginDTO
    {
        public string UserName { get; set; }
        public int IdUser { get; set; }
        public EnumRoles IdRole { get; set; }
    }
}
