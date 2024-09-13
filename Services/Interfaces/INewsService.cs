using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Services.Interfaces
{
    public interface INewsService
    {
        bool CreateNew(NewsModel news, int idUser);
        bool DeleteNew(int idNew, int idUser);
        List<NewsModel> GetAllNews();
        NewsModel GetNewData(int idNew);
        bool UpdateNew(NewsModel news, int idUser, int idNew);
    }
}
