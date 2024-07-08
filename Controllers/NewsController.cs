using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController: ControllerBase
    {
        private INewsService _newsSvc;

        public NewsController(INewsService newsSvc) {
            _newsSvc = newsSvc;
        }


        [HttpPost("createNew/{idUser}")]
        public ActionResult<bool> CreateNew([FromBody] NewsModel news, int idUser)
        {
            
            return Ok(_newsSvc.CreateNew(news, idUser));
        }

        [HttpPost("updateNew/{idUser}")]
        public ActionResult<bool> UpdateNew([FromBody] NewsModel news, int idUser)
        {
            return Ok(_newsSvc.UpdateNew(news, idUser));
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

    }
}
