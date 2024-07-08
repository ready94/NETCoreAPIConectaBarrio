namespace NETCoreAPIConectaBarrio.Models
{
    public class UserLoggedIn
    {
        public int IdUser { get; set; }
        public string UserName { get; set; }
        public int IdLanguage { get; set; }
        public string LanguageKey { get; set; }
        public Guid IdSession {  get; set; }
    }
}
