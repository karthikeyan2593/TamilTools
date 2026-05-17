using Microsoft.AspNetCore.Mvc;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class FuturesPnLController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FuturesPnLViewModel());
        }

        [HttpPost]
        public IActionResult Index(FuturesPnLViewModel model)
        {
            if (model.BuyPrice > 0 &&
                model.SellPrice > 0 &&
                model.InvestmentAmount > 0)
            {
                decimal quantity =
                    model.InvestmentAmount / model.BuyPrice;

                decimal gross =
                    quantity * model.SellPrice;

                decimal fee =
                    gross * (model.TradingFee / 100);

                model.FinalAmount =
                    gross - fee;

                model.ProfitLoss =
                    model.FinalAmount -
                    model.InvestmentAmount;

                model.ROI =
                    (model.ProfitLoss /
                    model.InvestmentAmount) * 100;
            }

            return View(model);
        }
    }
}