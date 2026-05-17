using Microsoft.AspNetCore.Mvc;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class LiquidationPriceController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new LiquidationPriceViewModel());
        }

        [HttpPost]
        public IActionResult Index(
            LiquidationPriceViewModel model)
        {
            if (model.EntryPrice > 0 &&
                model.Leverage > 0)
            {
                decimal percent =
                    100 / model.Leverage;

                if (model.PositionType == "Long")
                {
                    model.LiquidationPrice =
                        model.EntryPrice -
                        ((model.EntryPrice * percent) / 100);
                }
                else
                {
                    model.LiquidationPrice =
                        model.EntryPrice +
                        ((model.EntryPrice * percent) / 100);
                }
            }

            return View(model);
        }
    }
}