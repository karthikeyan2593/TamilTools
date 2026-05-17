// Controllers/CryptoPositionSizeController.cs

using Microsoft.AspNetCore.Mvc;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class CryptoPositionSizeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new CryptoPositionSizeViewModel());
        }

        [HttpPost]
        public IActionResult Index(
            CryptoPositionSizeViewModel model)
        {
            model.RiskAmount =
                model.AccountBalance *
                (model.RiskPercent / 100);

            decimal stopDistance =
                Math.Abs(
                    model.EntryPrice -
                    model.StopLossPrice);

            if (stopDistance > 0)
            {
                model.Quantity =
                    model.RiskAmount /
                    stopDistance;

                model.PositionSize =
                    model.Quantity *
                    model.EntryPrice;

                model.PositionSize *=
                    model.Leverage;

                model.MaxLoss =
                    model.RiskAmount;
            }

            return View(model);
        }
    }
}