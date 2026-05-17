// Controllers/SipCalculatorController.cs

using Microsoft.AspNetCore.Mvc;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class SipCalculatorController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new SipCalculatorViewModel());
        }

        [HttpPost]
        public IActionResult Index(SipCalculatorViewModel model)
        {
            decimal monthlyRate =
                (model.ExpectedReturnRate / 100m) / 12m;

            int months =
                model.Years * 12;

            decimal futureValue =
                model.MonthlyInvestment *
                (
                    (
                        (decimal)Math.Pow(
                            (double)(1 + monthlyRate),
                            months
                        ) - 1
                    ) / monthlyRate
                ) * (1 + monthlyRate);

            model.InvestedAmount =
                model.MonthlyInvestment * months;

            model.TotalValue =
                Math.Round(futureValue, 2);

            model.EstimatedReturns =
                model.TotalValue - model.InvestedAmount;

            return View(model);
        }
    }
}