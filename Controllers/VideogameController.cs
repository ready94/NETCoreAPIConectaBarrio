using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NETCoreAPIConectaBarrio.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideogameController : ControllerBase
    {
        // GET: api/<_VideogameController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<_VideogameController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<_VideogameController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<_VideogameController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<_VideogameController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
