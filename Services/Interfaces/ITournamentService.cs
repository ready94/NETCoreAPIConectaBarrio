using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ITournamentService
    {
        bool CreateTournament(TournamentModel tournament);
        bool DeleteTournament(int idTournament);
        List<TournamentDTO> GetAllTournaments();
        TournamentDTO GetTournamentData(int idTournament);
        bool UpdatePlayerNumbers(TournamentModel tournament);
        bool UpdateTournament(TournamentModel tournament);
    }
}
