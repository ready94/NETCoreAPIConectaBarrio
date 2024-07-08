using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.DTOs
{
    public class UserDTO
    {
        public EnumRoles IdRole { get; set; }
        public int IdUser { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? CultureKey { get; set; }
    }
}
