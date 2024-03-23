using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface INewsService
    {
        bool CreateNew(NewsModel news);
        bool DeleteNew(int idNew);
        List<NewsDTO> GetAllNews();
        NewsDTO GetNewData(int idNew);
        bool UpdateNew(NewsModel news);
    }
}
