using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.Models
{
    public class TournamentModel
    {
        public int IdTournament { get; set; }
        public EnumTournamentTypes IdTournamentType { get; set; }
        public string Name { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int ModificationUser { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int ConfirmedPlayers { get; set; }
        public bool Active { get; set; }

        public TournamentModel() { }

        public TournamentModel(MySqlDataReader dr)
        {
            IdTournament = dr.GetInt32("IDTOURNAMENT");
            IdTournamentType = (EnumTournamentTypes)dr.GetInt32("IDTOURNAMENT_TYPE");
            Name = dr.GetString("NAME");
            CreationUser = dr.GetInt32("CREATION_USER");
            CreationDate = dr.GetDateTime("CREATION_DATE");
            ModificationUser = dr.GetInt32("MODIFICATION_USER");
            ModificationDate = dr.GetDateTime("MODIFICATION_DATE");
            StartDate = dr.GetDateTime("START_DATE");
            EndDate = dr.GetDateTime("END_DATE");
            MinPlayers = dr.GetInt32("MIN_PLAYERS");
            MaxPlayers = dr.GetInt32("MAX_PLAYERS");
            ConfirmedPlayers = dr.GetInt32("CONFIRMED_PLAYERS");
            Active = dr.GetBoolean("ACTIVE");
        }
    }
}
