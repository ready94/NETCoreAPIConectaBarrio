using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;
using System.Diagnostics;

namespace NETCoreAPIConectaBarrio.Services
{
    public class ActivityService : IActivityService
    {
        private const string TABLE = "SYS_T_EVENTS";
        private IUserService _userSvc;

        public ActivityService(IUserService userSvc)
        {
            _userSvc = userSvc;
        }

        public bool CreateActivity(ActivityModel activity)
        {
            string[] fields = ["IDEVENT_TYPE", "IDEVENT_SUBCATEGORY", "LOCATION", "MAX_PERSON", "ACTUAL_PERSON", "CREATION_USER", "EVENT_DATE", "CREATION_DATE"];
            object[] values = [activity.IdEventType, activity.IdEventSubcategory, activity.Location, activity.MaxPerson,
                                activity.ActualPerson, activity.CreationUser, activity.EventDate, DateTime.Now];
            return SQLConnectionHelper.InsertBBDD(TABLE, fields, values);
        }

        public bool DeleteActivity(int idactivity, int idUser)
        {
            if (this._userSvc.GetUserRole(idUser) == EnumRoles.ADMIN)
                return SQLConnectionHelper.DeleteBBDD(TABLE, ["IDEVENT"], [idactivity], [SQLRelationType.EQUAL]);
            else
                return false;
            
        }

        public ActivityModel GetEventByIdEvent(int idactivity)
        {
            DataRow? row = SQLConnectionHelper.GetResult(TABLE, ["IDEVENT"], [idactivity], [SQLRelationType.EQUAL]);

            return new ActivityModel(row);
        }

        public bool UpdatePlayerNumbers(ActivityModel activity)
        {

            DataRow row = SQLConnectionHelper.GetResult(TABLE, ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);
            if(row != null)
            {
                int max = row.Field<int>("MAX_PERSON");
                if (activity.ActualPerson > max)
                    return false;
            }

            bool res = SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTUAL_PERSON"], [activity.ActualPerson], ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);


            return res;
        }

        public bool UpdateEvent(ActivityModel activity, int idUser)
        {
            string[] fields = ["IDEVENT_TYPE", "IDEVENT_SUBCATEGORY", "LOCATION", "MAX_PERSON", "ACTUAL_PERSON", "EVENT_DATE"];
            object[] values = [activity.IdEventType, activity.IdEventSubcategory, activity.Location, activity.MaxPerson,
                                activity.ActualPerson, activity.EventDate];

            bool res = SQLConnectionHelper.UpdateBBDD(TABLE, fields, values, ["IDEVENT"], [activity.IdEvent], [SQLConnectionHelper.EQUAL]);

            return res;
        }

        public List<ActivityModel> GetAllEventsFiltered(FilterModel filters)
        {
            List<ActivityModel> res = [];

            DataTable dt = SQLConnectionHelper.GetResultTable("VIEW_AVAILABLE_EVENTS", filters.fields, filters.values, filters.relations);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    res.Add(new ActivityModel(row));
                }
            }

            return res;
        }

        public List<ActivityModel> GetAllAvailableEvents()
        {
            List<ActivityModel> res = [];

            DataTable dt = SQLConnectionHelper.GetResultTable("VIEW_AVAILABLE_EVENTS");
            if(dt != null)
            {
                foreach(DataRow row in dt.Rows)
                {
                    res.Add(new ActivityModel(row));
                }
            }
            return res;
        }
    }
}
