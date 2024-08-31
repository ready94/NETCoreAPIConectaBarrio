using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data.Entity;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private IActivityService _activityService;
        public ActivityController(IActivityService activitySvc)
        {
            this._activityService = activitySvc;
        }


        [HttpGet("")]
        public ActionResult<List<ActivityModel>> GetAllAvailableEvents()
        {
            List<ActivityModel> result = [];
            try
            {
                result = this._activityService.GetAllAvailableEvents();
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllAvailableEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPost("getFilteredEvents")]
        public ActionResult<List<ActivityModel>> GetAllFilteredEvents([FromBody] FilterModel filters)
        {
            List<ActivityModel> result = [];
            try
            {
                result = this._activityService.GetAllEventsFiltered(filters);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpGet("getEventByIdEvent/{idEvent}")]
        public ActionResult<ActivityModel> GetEventByIdEvent(int idEvent)
        {
            ActivityModel result = null;
            try
            {
                result = this._activityService.GetEventByIdEvent(idEvent);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetEventByIdEvent() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPost("createEvent")]
        public ActionResult<bool> CreateEvent([FromBody] ActivityModel activity)
        {
            bool result = false;
            try
            {
                result = this._activityService.CreateActivity(activity);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpDelete("deleteEventByIdEvent/{idEvent}/{idUser}")]
        public ActionResult<bool> DeleteEvent(int idEvent, int idUser)
        {
            bool result = false;
            try
            {
                result = this._activityService.DeleteActivity(idEvent, idUser);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPut("updateNumberOfPlayers/{idUser}")]
        public ActionResult<bool> UpdateNumberOfPlayers([FromBody] ActivityModel activity, int idUser)
        {
            bool result = false;
            try
            {
                result = this._activityService.UpdatePlayerNumbers(activity, idUser);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPut("removeNumberOfPlayers/{idUser}")]
        public ActionResult<bool> RemoveNumberOfPlayers([FromBody] ActivityModel activity, int idUser)
        {
            bool result = false;
            try
            {
                result = this._activityService.RemovePlayerNumbers(activity, idUser);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPut("updateEvent/{idUser}")]
        public ActionResult<bool> UpdateEvent([FromBody] ActivityModel activity, int idUser)
        {
            bool result = false;
            try
            {
                result = this._activityService.UpdateEvent(activity, idUser);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpGet("getEventTypes")]
        public ActionResult<List<EventTypeDTO>> GetEventTournamentTypes()
        {
            List<EventTypeDTO> res = [];

            try
            {
                res = this._activityService.GetEventTournamentTypes();
            }
            catch(Exception Exc) { }
            return res;
        }

        [HttpGet("getEventCategories")]
        public ActionResult<List<EventCategoryDTO>> GetEventCategories()
        {
            List<EventCategoryDTO> res = [];

            try
            {
                res = this._activityService.GetEventCategories();
            }
            catch (Exception Exc) { }
            return res;
        }

        [HttpGet("getEventSubcategories")]
        public ActionResult<List<EventSubCategoryDTO>> GetEventSubCategories()
        {
            List<EventSubCategoryDTO> res = [];

            try
            {
                res = this._activityService.GetEventSubCategories();
            }
            catch (Exception Exc) { }
            return res;
        }

        [HttpGet("getAllEventsByUser/{idUser}")]
        public ActionResult<List<int>> GetAllEventsByUser(int idUser)
        {
            List<int> res = [];

            try
            {
                res = this._activityService.GetAllEventsByUser(idUser);
            }
            catch (Exception Exc) { }
            return res;
        }

    }
}


/*
      bool UpdatePlayerNumbers(ActivityModel activity);
        bool UpdateEvent(ActivityModel activity, int idUser);
 */