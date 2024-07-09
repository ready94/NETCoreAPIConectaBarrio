using Microsoft.AspNetCore.Mvc;
using NETCoreAPIConectaBarrio.DTOs;
using NETCoreAPIConectaBarrio.Models;
using NETCoreAPIConectaBarrio.Services.Interfaces;
using System.Text.Json;

namespace NETCoreAPIConectaBarrio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GoogleNewsController : ControllerBase
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public GoogleNewsController()
        {
        }

        [HttpGet("getAllNews")]
        public async Task<IActionResult> GetAllNews()
        {
            var query = "Barrio del Pilar Madrid";
            var fromDate = DateTime.Now.AddDays(-1).ToString("dd-MM-yyyy");
            var apiKey = "f32ef93528414f4082a753abd51e79bc";
            var url = $"https://newsapi.org/v2/everything?q={query}&from={fromDate}&sortBy=publishedAt&apiKey={apiKey}";
            var request = new HttpRequestMessage(HttpMethod.Get, url) ;
            request.Headers.UserAgent.ParseAdd("ConectaBarrio/1.0");
            var response = await _httpClient.SendAsync(request);

            if(!response.IsSuccessStatusCode)
            {
                return StatusCode((int)response.StatusCode, await response.Content.ReadAsStringAsync());
            }

            var newsContent = await response.Content.ReadAsStringAsync();
            var newsResponse = JsonSerializer.Deserialize<NewsResponse>(newsContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return Ok(newsContent);
        }
    }
}
