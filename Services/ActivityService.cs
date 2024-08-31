using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using NETCoreAPIConectaBarrio.Enums;
using System.Data;
using System.Diagnostics;
using NETCoreAPIConectaBarrio.DTOs;

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
            object[] values = [activity.IdEventType, activity.IdEventSubCategory, activity.Location, activity.MaxPerson,
                                1, activity.CreationUser, activity.EventDate, DateTime.Now];

            int res = SQLConnectionHelper.InsertIdentityBBDD(TABLE, fields, values);

            SQLConnectionHelper.InsertBBDD("sys_rel_user_event", ["IDUSER", "IDEVENT"], [activity.CreationUser, res]);

            return true;
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

        public bool UpdatePlayerNumbers(ActivityModel activity, int idUser)
        {
            int actuales = activity.ActualPerson + 1;
            bool res = false;

            DataRow row = SQLConnectionHelper.GetResult(TABLE, ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);
            if (row != null)
            {
                int max = row.Field<int>("MAX_PERSON");
                if (activity.ActualPerson > max)
                    return false;

                res = SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTUAL_PERSON"], [actuales], ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);
                SQLConnectionHelper.InsertBBDD("sys_rel_user_event", ["IDUSER", "IDEVENT"], [idUser, activity.IdEvent]);
            }

            return res;
        }

       public bool RemovePlayerNumbers(ActivityModel activity, int idUser)
       {
            int actuales = activity.ActualPerson - 1;
            bool res = false;

            DataRow row = SQLConnectionHelper.GetResult(TABLE, ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);
            if (row != null)
            { 
                res = SQLConnectionHelper.UpdateBBDD(TABLE, ["ACTUAL_PERSON"], [actuales], ["IDEVENT"], [activity.IdEvent], [SQLRelationType.EQUAL]);
                SQLConnectionHelper.DeleteBBDD("sys_rel_user_event", ["IDUSER", "IDEVENT"], [idUser, activity.IdEvent], [SQLRelationType.EQUAL, SQLRelationType.EQUAL]);
            }

            return res;
       }

        public bool UpdateEvent(ActivityModel activity, int idUser)
        {
            string[] fields = ["IDEVENT_TYPE", "IDEVENT_SUBCATEGORY", "LOCATION", "MAX_PERSON", "ACTUAL_PERSON", "EVENT_DATE"];
            object[] values = [activity.IdEventType, activity.IdEventSubCategory, activity.Location, activity.MaxPerson,
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
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    res.Add(new ActivityModel(row));
                }
            }
            return res;
        }
        public List<EventTypeDTO> GetEventTournamentTypes()
        {
            List<EventTypeDTO> res = [];
            DataTable dt = SQLConnectionHelper.GetResultTable("sys_m_events_type");
            foreach (DataRow row in dt.Rows)
            {
                EventTypeDTO type = new()
                {
                    IdEventType = row.Field<int>("IDEVENT_TYPE"),
                    EventType = row.Field<string>("EVENT_TYPE")
                };
                res.Add(type);
            }

            return res;
        }
        public List<EventCategoryDTO> GetEventCategories()
        {
            List<EventCategoryDTO> res = [];
            DataTable dt = SQLConnectionHelper.GetResultTable("sys_m_events_category");
            foreach (DataRow row in dt.Rows)
            {
                EventCategoryDTO type = new()
                {
                    IdEventCategory = row.Field<int>("IDEVENT_CATEGORY"),
                    EventCategory = row.Field<string>("EVENT_CATEGORY")
                };

                res.Add(type);
            }

            return res;
        }
        public List<EventSubCategoryDTO> GetEventSubCategories()
        {
            List<EventSubCategoryDTO> res = [];
            DataTable dt = SQLConnectionHelper.GetResultTable("sys_m_events_subcategory");
            foreach (DataRow row in dt.Rows)
            {
                EventSubCategoryDTO type = new()
                {
                    IdEventCategory = row.Field<int>("IDEVENT_CATEGORY"),
                    IdEventSubCategory = row.Field<int>("IDEVENT_SUBCATEGORY"),
                    EventSubCategory = row.Field<string>("EVENT_SUBCATEGORY")
                };
                res.Add(type);
            }

            return res;
        }


        public List<int> GetAllEventsByUser(int idUser)
        {
            List<int> res = [];
            try
            {
                DataTable dt = SQLConnectionHelper.GetResultTable("sys_rel_user_event", ["IDUSER"], [idUser], [SQLRelationType.EQUAL]);
                foreach (DataRow row in dt.Rows)
                {
                    res.Add(row.Field<int>("IDEVENT"));
                }

            }catch(Exception exc) { }
            return res;
        }

    }
}
