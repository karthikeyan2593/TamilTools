using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TamilTools.Controllers
{
    public class CurrencyController : Controller
    {
        private readonly HttpClient _httpClient;

        public CurrencyController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Convert(double amount, string fromCurrency, string toCurrency)
        {
            try
            {
                var url = $"https://open.exchangerate-api.com/v6/latest/{fromCurrency}";
                var response = await _httpClient.GetStringAsync(url);
                var json = JsonDocument.Parse(response);
                var rates = json.RootElement.GetProperty("rates");

                double rate = rates.GetProperty(toCurrency).GetDouble();
                double result = amount * rate;

                ViewBag.Amount = amount;
                ViewBag.FromCurrency = fromCurrency;
                ViewBag.ToCurrency = toCurrency;
                ViewBag.Rate = Math.Round(rate, 4);
                ViewBag.Result = Math.Round(result, 2);
                ViewBag.InputAmount = amount;
            }
            catch
            {
                ViewBag.Error = "தற்போது exchange rate கிடைக்கவில்லை. மீண்டும் try பண்ணுங்கள்!";
            }

            return View("Index");
        }
    }
}