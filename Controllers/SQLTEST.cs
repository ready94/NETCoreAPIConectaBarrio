using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SQLTEST : ControllerBase
    {
        [HttpGet("")]
        public ActionResult<string[]> TEST()
        {
            return SQLConnectionHelper.TEST();
        }

        [HttpPost("insert")]
        public ActionResult<bool> InsertTest([FromBody] string table)
        {
            string[] fields = ["IDTEST", "NAME", "CREATIONDATE"];
            List<object> values = [3, "TEST_SWAGGER", DateOnly.FromDateTime(DateTime.Now)];

            return SQLConnectionHelper.InsertBBDD(table, fields, values.ToArray());
        }

        [HttpPost("delete")]
        public ActionResult<bool> DeleteTest([FromBody] string table)
        {
            string[] fields = ["IDTEST"];
            object[] values = [3];
            string[] relations = ["="];
            return SQLConnectionHelper.DeleteBBDD(table, fields, values, relations);
        }

        [HttpPost("getResult")]
        public ActionResult<string> GetResultTest([FromBody] string table)
        {
            string[] fields = ["IDTEST"];
            object[] values = [3];
            string[] relations = ["="];
            MySqlDataReader res = SQLConnectionHelper.GetResult(table, fields, values, relations);

            string result = "";
            while (res.Read())
            {
                result += res.ToString();
            }
            return result;
        }

        [HttpPost("update")]
        public ActionResult<bool> UpdateTest([FromBody] string table)
        {
            string[] fields = ["NAME"];
            object[] values = ["TEST_SWAGGER_UPDATE"];
            string[] fieldsFilter = ["IDTEST"];
            object[] valuesFilter = [3];
            return SQLConnectionHelper.UpdateBBDD(table, fields, values, fieldsFilter, valuesFilter);
        }

    }
}
