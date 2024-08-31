using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface ISharedService
    {
        List<OptionsModel> GetMenuOptions();
    }
}
