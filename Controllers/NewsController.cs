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

        [HttpPost("createNew")]
        public ActionResult<bool> CreateNew([FromBody] NewsModel news)
        {
            return Ok(_newsSvc.CreateNew(news));
        }

        [HttpPost("updateNew")]
        public ActionResult<bool> UpdateNew([FromBody] NewsModel news)
        {
            return Ok(_newsSvc.UpdateNew(news));
        }

        [HttpGet("deleteNew")]
        public ActionResult<bool> DeleteNew(int idNew)
        {
            return Ok(_newsSvc.DeleteNew(idNew));
        }

        [HttpGet("getNewData/{idNew}")]
        public ActionResult<NewsDTO> GetNewData(int idNew)
        {
            return Ok(_newsSvc.GetNewData(idNew));
        }

        [HttpGet("getAllNews")]
        public ActionResult<List<NewsDTO>> GetAllNews()
        {
            return Ok(_newsSvc.GetAllNews());
        }

    }
}
