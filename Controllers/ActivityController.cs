using Microsoft.AspNetCore.Mvc;
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
            catch(Exception exc)
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

        [HttpPut("updateNumberOfPlayers")]
        public ActionResult<bool> UpdateNumberOfPlayers([FromBody] ActivityModel activity)
        {
            bool result = false;
            try
            {
                result = this._activityService.UpdatePlayerNumbers(activity);
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

    }
}


/*
      bool UpdatePlayerNumbers(ActivityModel activity);
        bool UpdateEvent(ActivityModel activity, int idUser);
 */