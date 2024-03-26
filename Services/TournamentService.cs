using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class TournamentService : ITournamentService
    {
        private const string TABLE = "SYS_T_TOURNAMENTS";
        private IUserService _userSvc;

        public TournamentService(IUserService userSvc)
        {
            _userSvc = userSvc;
        }

        public bool CreateTournament(TournamentModel tournament)
        {
            string[] fields = ["IDTOURNAMENT_TYPE", "NAME", "CREATION_USER", "CREATION_DATE",
                                "START_DATE", "END_DATE", "MIN_PLAYERS", "MAX_PLAYERS", "ACTIVE"];
            object[] values = [tournament.IdTournamentType, tournament.Name, tournament.CreationUser, DateTime.Now,
                                tournament.StartDate, tournament.EndDate, tournament.MinPlayers, tournament.MaxPlayers, tournament.Active];
            return SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
        }

        public bool DeleteTournament(int idTournament, int idUser)
        {
            if (this._userSvc.GetUserRole(idUser) == Enums.EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD(TABLE, ["IDTOURNAMENT"], [idTournament], [SQLRelationType.EQUAL]);
            else
                return SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTIVE"], [false], ["IDTOURNAMENT"], [idTournament]);
        }

        public List<TournamentModel> GetAllTournaments()
        {
            List<TournamentModel> res = [];
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE, [], [], []);

            foreach (DataRow row in dt.Rows)
                res.Add(new TournamentModel(row));

            return res;
        }

        public TournamentModel GetTournamentData(int idTournament)
        {
            TournamentModel res = null;
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE, ["IDTOURNAMENT"], [idTournament], [SQLRelationType.EQUAL]);

            if (dt != null && dt.Rows.Count > 0)
                res = new TournamentModel(dt.Rows[0]);

            return res;
        }

        public bool UpdatePlayerNumbers(TournamentModel tournament)
        {
            return SQLConnectionHelper.UpdateBBDD(TABLE, ["CONFIRMED_PLAYERS"], [tournament.ConfirmedPlayers], ["IDTOURNAMENT"], [tournament.IdTournament]);
        }

        public bool UpdateTournament(TournamentModel tournament, int idUser)
        {
            string[] fields = ["IDTOURNAMENT_TYPE", "NAME", "MODIFICATION_USER", "MODIFICATION_DATE", "START_DATE", "END_DATE", "MIN_PLAYERS", "MAX_PLAYERS", "CONFIRMED_PLAYERS", "ACTIVE"];
            object[] values = [tournament.IdTournamentType, tournament.Name, idUser, DateTime.Now, tournament.StartDate, tournament.EndDate, tournament.MinPlayers, tournament.MaxPlayers, tournament.ConfirmedPlayers, tournament.Active];
            return SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, ["IDTOURNAMENT"], [tournament.IdTournament]);
        }
    }
}
