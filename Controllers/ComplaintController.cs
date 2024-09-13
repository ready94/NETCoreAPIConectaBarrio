using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ComplaintController: ControllerBase
    {
        private IComplaintService _complaintSvc;

        public ComplaintController(IComplaintService complaintSvc)
        {
            _complaintSvc = complaintSvc;
        }

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

    }
}
