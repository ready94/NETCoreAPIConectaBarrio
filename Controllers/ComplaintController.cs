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

        [HttpPost("createComplaint")]
        public ActionResult<bool> CreateComplaint([FromBody] ComplaintModel complaint)
        {
            return Ok(_complaintSvc.CreateComplaint(complaint));
        }

        [HttpPost("updateComplaint")]
        public ActionResult<bool> UpdateComplaint([FromBody] ComplaintModel complaint)
        {
            return Ok(_complaintSvc.UpdateComplaint(complaint));
        }

        [HttpGet("deleteComplaint/{idComplaint}")]
        public ActionResult<bool> DeleteComplaint(int idComplaint)
        {
            return Ok(_complaintSvc.DeleteComplaint(idComplaint));
        }

        [HttpGet("getComplaint/{idComplaint}")]
        public ActionResult<ComplaintDTO> GetComplaint(int idComplaint)
        {
            return Ok(_complaintSvc.GetComplaint(idComplaint));
        }

        [HttpGet("getAllComplaints")]
        public ActionResult<List<ComplaintDTO>> GetAllComplaints()
        {
            return Ok(_complaintSvc.GetAllComplaints());
        }
    }
}
