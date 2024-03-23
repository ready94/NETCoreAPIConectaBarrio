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
        private ITournamentService _tournamentSvc;

        public TournamentController(ITournamentService tournamentSvc)
        {
            _tournamentSvc = tournamentSvc;
        }

        [HttpPost("createTournament")]
        public ActionResult<bool> CreateTournament([FromBody] TournamentModel tournament)
        {
            return Ok(_tournamentSvc.CreateTournament(tournament));
        }

        [HttpPost("updateTournament")]
        public ActionResult<bool> UpdateTournament([FromBody] TournamentModel tournament)
        {
            return Ok(_tournamentSvc.UpdateTournament(tournament));
        }

        [HttpGet("deleteTournament/{idTournament}")]
        public ActionResult<bool> DeleteTournament(int idTournament)
        {
            return Ok(_tournamentSvc.DeleteTournament(idTournament));
        }

        [HttpGet("getTournamentData/{idTournament}")]
        public ActionResult<TournamentDTO> GetTournamentData(int idTournament)
        {
            return Ok(_tournamentSvc.GetTournamentData(idTournament));
        }

        [HttpGet("getAllTournaments")]
        public ActionResult<List<TournamentDTO>> GetAllTournaments()
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
