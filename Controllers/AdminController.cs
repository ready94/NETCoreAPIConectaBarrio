using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private IUserService _userSvc;
        private INewsService _newsSvc;
        private IActivityService _activitySvc;
        private IComplaintService _complaintSvc;
        private IAdminService _adminSvc;

        public AdminController(IUserService userSvc, INewsService newsSvc, IActivityService activitySvc, IComplaintService complaintSvc, IAdminService adminSvc)
        {
            _userSvc = userSvc;
            _newsSvc = newsSvc;
            _activitySvc = activitySvc;
            _complaintSvc = complaintSvc;
            _adminSvc = adminSvc;
        }

        [HttpGet("getAdminOptions")]
        public ActionResult<List<AdminOptionModel>> GetAdminOptions()
        {
            return Ok(_adminSvc.GetAdminOptions());
        }

        #region USERS

        [HttpPost("createUser/{idAdmin}")]
        public ActionResult<bool> CreateUser([FromBody] NewUserModel user, int idAdmin)
        {
            return Ok(_userSvc.CreateUser(user, idAdmin));
        }

        [HttpPost("updateUser/{idAdmin}/{idUser}")]
        public ActionResult<bool> UpdateUser([FromBody] UserModel user, int idAdmin, int idUser)
        {
            return Ok(_userSvc.UpdateUser(user, idAdmin, idUser));
        }

        [HttpPost("blockUser/{idAdmin}")]
        public ActionResult<bool> BlockUser([FromBody] UserModel user, int idAdmin)
        {
            return Ok(_userSvc.BlockUser(user, idAdmin));
        }

        [HttpPost("unblockUser/{idAdmin}")]
        public ActionResult<bool> UnblockUser([FromBody] UserModel user, int idAdmin)
        {
            return Ok(_userSvc.UnblockUser(user, idAdmin));
        }

        [HttpGet("getUserData/{idUser}")]
        public ActionResult<UserDTO> GetUserData(int idUser)
        {
            return Ok(_userSvc.GetUserData(idUser));
        }

        [HttpGet("getAllUsers")]
        public ActionResult<List<UserDTO>> GetAllUsers()
        {
            return Ok(_userSvc.GetAllUsers());
        }

        [HttpPost("deleteUser/{idAdmin}")]
        public ActionResult<bool> DeleteUser([FromBody] UserModel user, int idAdmin)
        {
            return Ok(_userSvc.DeleteUser(user, idAdmin));
        }

        #endregion

        #region NEWS

        [HttpPost("createNew/{idUser}")]
        public ActionResult<bool> CreateNew([FromBody] NewsModel news, int idUser)
        {
            return Ok(_newsSvc.CreateNew(news, idUser));
        }

        [HttpPost("updateNew/{idUser}/{idNew}")]
        public ActionResult<bool> UpdateNew([FromBody] NewsModel news, int idUser, int idNew)
        {
            return Ok(_newsSvc.UpdateNew(news, idUser, idNew));
        }

        [HttpPost("deleteNew/{idUser}")]
        public ActionResult<bool> DeleteNew([FromBody] int idNew, int idUser)
        {
            return Ok(_newsSvc.DeleteNew(idNew, idUser));
        }

        [HttpGet("getNewData/{idNew}")]
        public ActionResult<NewsModel> GetNewData(int idNew)
        {
            return Ok(_newsSvc.GetNewData(idNew));
        }

        [HttpGet("getAllNews")]
        public ActionResult<List<NewsModel>> GetAllNews()
        {
            return Ok(_newsSvc.GetAllNews());
        }
        #endregion

        #region COMPLAINTS

        [HttpPost("createComplaint/{idUser}")]
        public ActionResult<bool> CreateComplaint([FromBody] ComplaintDTO complaint, int idUser)
        {
            return Ok(_complaintSvc.CreateComplaint(complaint, idUser));
        }

        [HttpPost("updateComplaint/{idUser}/{idComplaint}")]
        public ActionResult<bool> UpdateComplaint([FromBody] ComplaintModel complaint, int idUser, int idComplaint)
        {
            return Ok(_complaintSvc.UpdateComplaint(complaint, idUser, idComplaint));
        }

        [HttpPost("deleteComplaint/{idUser}")]
        public ActionResult<bool> DeleteComplaint([FromBody] int idComplaint, int idUser)
        {
            return Ok(_complaintSvc.DeleteComplaint(idUser, idComplaint));
        }

        [HttpGet("getComplaint/{idComplaint}")]
        public ActionResult<ComplaintModel> GetComplaint(int idComplaint)
        {
            return Ok(_complaintSvc.GetComplaint(idComplaint));
        }

        [HttpGet("getAllComplaints")]
        public ActionResult<List<ComplaintModel>> GetAllComplaints()
        {
            return Ok(_complaintSvc.GetAllComplaints());
        }

        #endregion

        #region ACTIVITIES

        [HttpGet("getAllActivities")]
        public ActionResult<List<ActivityModel>> GetAllAvailableEvents()
        {
            List<ActivityModel> result = [];
            try
            {
                result = this._activitySvc.GetAllAvailableEvents();
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
                result = this._activitySvc.GetAllEventsFiltered(filters);
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
                result = this._activitySvc.GetEventByIdEvent(idEvent);
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
                result = this._activitySvc.CreateActivity(activity);
            }
            catch (Exception exc)
            {
                throw new Exception($"Error en GetAllFilteredEvents() => " + exc.Message, exc);
            }
            return result;
        }

        [HttpPost("deleteActivity/{idUser}")]
        public ActionResult<bool> DeleteEvent([FromBody] int idEvent, int idUser)
        {
            bool result = false;
            try
            {
                result = this._activitySvc.DeleteActivity(idEvent, idUser);
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
                result = this._activitySvc.UpdatePlayerNumbers(activity, idUser);
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
                result = this._activitySvc.RemovePlayerNumbers(activity, idUser);
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
                result = this._activitySvc.UpdateEvent(activity, idUser);
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
                res = this._activitySvc.GetEventTournamentTypes();
            }
            catch (Exception Exc) { }
            return res;
        }

        [HttpGet("getEventCategories")]
        public ActionResult<List<EventCategoryDTO>> GetEventCategories()
        {
            List<EventCategoryDTO> res = [];

            try
            {
                res = this._activitySvc.GetEventCategories();
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
                res = this._activitySvc.GetEventSubCategories();
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
                res = this._activitySvc.GetAllEventsByUser(idUser);
            }
            catch (Exception Exc) { }
            return res;
        }

    }

    #endregion
}
