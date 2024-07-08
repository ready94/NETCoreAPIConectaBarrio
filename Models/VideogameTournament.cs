using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class VideogameTournament
    {
        public int IdTournament { get; set; }
        public string VideoGameTitle { get; set; }
        public string SystemPlatform { get; set; }
        public string? Place {  get; set; }
        public float? Price { get; set; }
        public string? Winner { get; set; }
        public int? IdUserWinner { get; set; }
        public string? SecondPLace { get; set; }
        public int? IdUserSecondPlace { get; set; }

        public VideogameTournament() { }

        public VideogameTournament(DataRow row)
        {
            IdTournament = row.Field<int>("IDTOURNAMENT");
            VideoGameTitle = row.Field<string>("VIDEOGAME_TITLE");
            SystemPlatform = row.Field<string>("SYSTEM_PLATFORM");
            Place = row.Field<string?>("PLACE");
            Price = row.Field<float?>("PRICE");
            Winner = row.Field<string?>("WINNER");
            IdUserWinner = row.Field<int?>("IDUSER_WINNER");
            SecondPLace = row.Field<string?>("SECOND_PLACE");
            IdUserSecondPlace = row.Field<int?>("IDUSER_SECOND_PLACE");
        }   
    }
}
