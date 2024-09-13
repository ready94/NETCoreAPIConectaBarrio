using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using System.Data.Entity;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface IActivityService
    {
        bool CreateActivity(ActivityModel activity);
        bool DeleteActivity(int idEvent, int idUser);
        List<ActivityModel> GetAllEventsFiltered(FilterModel filters);
        List<ActivityModel> GetAllAvailableEvents();
        ActivityModel GetEventByIdEvent(int idEvent);
        bool UpdatePlayerNumbers(ActivityModel activity, int idUser);
        bool RemovePlayerNumbers(ActivityModel activity, int idUser);
        bool UpdateEvent(ActivityModel activity, int idUser);

        List<EventTypeDTO> GetEventTournamentTypes();
        List<EventCategoryDTO> GetEventCategories();
        List<EventSubCategoryDTO> GetEventSubCategories();

        List<int> GetAllEventsByUser(int idUser);
    }
}
