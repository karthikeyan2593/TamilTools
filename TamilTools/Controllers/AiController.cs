using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace TamilTools.Controllers
{
    public class AiController : Controller
    {
        private readonly HttpClient _httpClient;

        public AiController()
        {
            _httpClient = new HttpClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Ask(string prompt)
        {
            var apiKey = "YOUR_GEMINI_API_KEY";

            var requestBody = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new { text = prompt }
                        }
                    }
                }
            };

            var json = JsonConvert.SerializeObject(requestBody);

            var content = new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

            var response = await _httpClient.PostAsync(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={apiKey}",
                content);

            var result = await response.Content.ReadAsStringAsync();

            return Content(result, "application/json");
        }
    }
}