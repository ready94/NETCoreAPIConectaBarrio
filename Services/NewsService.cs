using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;

namespace NETCoreAPIConectaBarrio.Services
{
    public class NewsService : INewsService
    {
        private const string TABLE = "SYS_T_NEWS";
        private IUserService _userService;

        public NewsService(IUserService userService)
        {
            _userService = userService;
        }

        public bool CreateNew(NewsModel news, int idUser)
        {
            string[] fields = ["IDCATEGORY", "NAME", "DESCRIPTION", "CREATION_USER", "CREATION_DATE", "START_DATE", "END_DATE", "ACTIVE"];
            object[] values = [news.IdCategory, news.Name, news.Description, idUser, DateTime.Now, news.StartDate, news.EndDate, news.Active];
            return SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
        }

        public bool DeleteNew(int idNew, int idUser)
        {
            if (this._userService.GetUserRole(idUser) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD(TABLE, ["IDNEW"], [idNew], [SQLRelationType.EQUAL]);
            else
                return SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTIVE"], [false], ["IDNEW"], [idNew]);
        }

        public List<NewsModel> GetAllNews()
        {
            List<NewsModel> res = [];
            DataTable dt = SQLConnectionHelper.GetResultTable(TABLE);
           
            foreach (DataRow row in dt.Rows)
                res.Add(new NewsModel(row));

            return res;
        }

        public NewsModel? GetNewData(int idNew)
        {
            NewsModel res = null;
            DataRow? row = SQLConnectionHelper.GetResult(TABLE, ["IDNEW"], [idNew], [SQLRelationType.EQUAL]);
            res = new NewsModel(row);

            return res;
        }

        public bool UpdateNew(NewsModel news, int idUser)
        {
            string[] fields = ["IDCATEGORY", "NAME", "DESCRIPTION", "MODIFICATION_USER", "MODIFICATION_DATE", "START_DATE", "END_DATE", "ACTIVE"];
            object[] values = [news.IdCategory, news.Name, news.Description, idUser, DateTime.Now, news.StartDate, news.EndDate, news.Active];
            return SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, ["IDNEW"], [news.IdNew]);
        }
    }
}
