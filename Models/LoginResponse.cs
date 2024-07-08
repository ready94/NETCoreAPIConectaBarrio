using NETCoreAPIConectaBarrio.DTOs;

namespace NETCoreAPIConectaBarrio.Models
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public UserDTO User { get; set; }
    }
}
