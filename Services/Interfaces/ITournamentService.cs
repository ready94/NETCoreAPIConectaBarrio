using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ITournamentService
    {
        bool CreateTournament(TournamentModel tournament);
        bool DeleteTournament(int idTournament, int idUser);
        List<TournamentModel> GetAllTournaments();
        TournamentModel GetTournamentData(int idTournament);
        bool UpdatePlayerNumbers(TournamentModel tournament);
        bool UpdateTournament(TournamentModel tournament, int idUser);
    }
}
