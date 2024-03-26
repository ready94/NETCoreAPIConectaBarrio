using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class TournamentModel
    {
        public int IdTournament { get; set; }
        public EnumTournamentTypes IdTournamentType { get; set; }
        public string? Name { get; set; }
        public int CreationUser { get; set; }
        public DateTime CreationDate { get; set; }
        public int? ModificationUser { get; set; }
        public DateTime? ModificationDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
        public int? ConfirmedPlayers { get; set; }
        public bool Active { get; set; }

        public TournamentModel() { }

        public TournamentModel(DataRow row)
        {
            IdTournament = row.Field<int>("IDTOURNAMENT");
            IdTournamentType = (EnumTournamentTypes)row.Field<int>("IDTOURNAMENT_TYPE");
            Name = row.Field<string?>("NAME");
            CreationUser = row.Field<int>("CREATION_USER");
            CreationDate = row.Field<DateTime>("CREATION_DATE");
            ModificationUser = row.Field<int?>("MODIFICATION_USER");
            ModificationDate = row.Field<DateTime?>("MODIFICATION_DATE");
            StartDate = row.Field<DateTime>("START_DATE");
            EndDate = row.Field<DateTime>("END_DATE");
            MinPlayers = row.Field<int>("MIN_PLAYERS");
            MaxPlayers = row.Field<int>("MAX_PLAYERS");
            ConfirmedPlayers = row.Field<int?>("CONFIRMED_PLAYERS");
            Active = row.Field<bool>("ACTIVE");
        }
    }
}
