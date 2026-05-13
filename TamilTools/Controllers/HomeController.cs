using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.Diagnostics;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> GeminiTest()
        {
            string apiKey = _configuration["GeminiApiKey"];

            var client = new RestClient(
                $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key={apiKey}"
            );

            var request = new RestRequest("", Method.Post);

            request.AddHeader("Content-Type", "application/json");

            var body = new
            {
                contents = new[]
                {
                    new
                    {
                        parts = new[]
                        {
                            new
                            {
                                text = "Hello Gemini!"
                            }
                        }
                    }
                }
            };

            request.AddJsonBody(body);

            var response = await client.ExecuteAsync(request);

            return Content(response.Content ?? "No response", "application/json");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}