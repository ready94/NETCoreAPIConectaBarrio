using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Helpers;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleNewsController : ControllerBase
    {
        public GoogleNewsController()
        {
        }

        [HttpGet("getAllNews")]
        public async Task<IActionResult> GetAllNews()
        {
            var handler = new SocketsHttpHandler
            {
                SslOptions = new System.Net.Security.SslClientAuthenticationOptions
                {
                    EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12
                },
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
            };

            using (HttpClient client = new HttpClient(handler))
            {
                client.DefaultRequestVersion = new Version(2, 0); 
                client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrHigher;

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (compatible; ConectaBarrio/1.0)");

                var query = "Barrio del Pilar Madrid";
                var fromDate = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");  
                var apiKey = "f32ef93528414f4082a753abd51e79bc";
                var url = $"https://newsapi.org/v2/everything?q={query}&from={fromDate}&sortBy=publishedAt&apiKey={apiKey}";

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
                }

                var newsContent = await response.Content.ReadAsStringAsync();
                return Ok(newsContent);
            }

        }


        [HttpPost("saveNews")]
        public ActionResult<bool> SaveGoogleNews([FromBody] List<GoogleNewsModel> googleNews)
        {
            bool res = false;

            try
            {

                string[] fields = ["AUTHOR", "CONTENT", "DESCRIPTION", "PUBLISHED_AT", "TITLE", "URL", "URL_TO_IMAGE"];
                object[] values = [];
                string[] fieldsCheck = ["AUTHOR", "TITLE"];
                string[] relations = [SQLRelationType.EQUAL, SQLRelationType.EQUAL];
                foreach (GoogleNewsModel google in googleNews)
                {
                    string authorFormatted = ReplaceSpecialCharacters(google.Author);
                    string contentFormatted = ReplaceSpecialCharacters(google.Content);
                    string descriptionFormatted = ReplaceSpecialCharacters(google.Description);
                    string titleFormatted = ReplaceSpecialCharacters(google.Title);

                    DataRow row = SQLConnectionHelper.GetResult("sys_t_google_news", fieldsCheck, [authorFormatted, titleFormatted], relations);
                    if (row == null || row.ItemArray.Length == 0)
                    {
                        values = [authorFormatted, contentFormatted, descriptionFormatted, google.PublishedAt, titleFormatted, google.Url, google.UrlToImage];
                        SQLConnectionHelper.InsertBBDD("sys_t_google_news", fields, values);
                    }
                }
                res = true;

            }
            catch (Exception exc) { throw new Exception($"Error en SaveGoogleNews() => " + exc.Message, exc); }

            return res;
        }

        private string ReplaceSpecialCharacters(string str)
        {
            string firstRes = str.Replace("\"", "\\\"");
            string secondRes = firstRes.Replace("'", "\\'");
            return secondRes;
        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                //if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_' || c == ' ')
                if (c != '\'' || c != '\"')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
