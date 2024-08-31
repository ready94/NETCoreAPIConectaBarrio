using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.DTOs
{
    public class NewUserModel
    {
        public string? Name {  get; set; }
        public string? Surname { get; set; }
        public string? Username {  get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public EnumRoles? IdRole {  get; set; }
    }
}
