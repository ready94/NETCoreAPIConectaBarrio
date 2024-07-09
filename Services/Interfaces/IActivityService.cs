using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IActivityService
    {
        bool CreateActivity(ActivityModel activity);
        bool DeleteActivity(int idActivity, int idUser);
        List<ActivityModel> GetAllEventsFiltered(FilterModel filters);
        List<ActivityModel> GetAllAvailableEvents();
        ActivityModel GetEventByIdEvent(int idEvent);
        bool UpdatePlayerNumbers(ActivityModel activity);
        bool UpdateEvent(ActivityModel activity, int idUser);
    }
}
