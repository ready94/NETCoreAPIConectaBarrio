using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TournamentController: ControllerBase   
    {
        private IActivityService _tournamentSvc;

        public TournamentController(IActivityService tournamentSvc)
        {
            _tournamentSvc = tournamentSvc;
        }

        [HttpPost("createTournament")]
        public ActionResult<bool> CreateTournament([FromBody] TournamentModel tournament)
        {
            return Ok(_tournamentSvc.CreateTournament(tournament));
        }

        [HttpPost("updateTournament/{idUser}")]
        public ActionResult<bool> UpdateTournament([FromBody] TournamentModel tournament, int idUser)
        {
            return Ok(_tournamentSvc.UpdateTournament(tournament, idUser));
        }

        [HttpPost("deleteTournament/{idUser}")]
        public ActionResult<bool> DeleteTournament([FromBody] int idTournament, int idUser)
        {
            return Ok(_tournamentSvc.DeleteTournament(idTournament, idUser));
        }

        [HttpGet("getTournamentData/{idTournament}")]
        public ActionResult<TournamentModel> GetTournamentData(int idTournament)
        {
            return Ok(_tournamentSvc.GetTournamentData(idTournament));
        }

        [HttpGet("getAllTournaments")]
        public ActionResult<List<TournamentModel>> GetAllTournaments()
        {
            return Ok(_tournamentSvc.GetAllTournaments());
        }

        [HttpPost("updatePlayerNumbers")]
        public ActionResult<bool> UpdatePlayerNumbers([FromBody] TournamentModel tournament)
        {
            return Ok(_tournamentSvc.UpdatePlayerNumbers(tournament));
        }

    }
}
