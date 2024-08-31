using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IComplaintService
    {
        bool CreateComplaint(ComplaintDTO complaint, int idUser);
        bool DeleteComplaint(int idUser, int idComplaint);
        List<ComplaintModel> GetAllComplaints();
        ComplaintModel? GetComplaint(int idComplaint);
        bool UpdateComplaint(ComplaintModel complaint, int idUser);
    }
}
