using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IComplaintService
    {
        bool CreateComplaint(ComplaintModel complaint);
        bool DeleteComplaint(int idComplaint);
        List<ComplaintDTO> GetAllComplaints();
        ComplaintDTO GetComplaint(int idComplaint);
        bool UpdateComplaint(ComplaintModel complaint);
    }
}
