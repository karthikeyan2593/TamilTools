using Microsoft.AspNetCore.Mvc;
using TamilTools.Models;

namespace TamilTools.Controllers
{
    public class FederalTaxController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(new FederalTaxViewModel());
        }

        [HttpPost]
        public IActionResult Index(FederalTaxViewModel model)
        {
            decimal tax = CalculateFederalTax(
                model.AnnualIncome,
                model.FilingStatus
            );

            model.FederalTax = tax;
            model.NetIncome = model.AnnualIncome - tax;

            if (model.AnnualIncome > 0)
            {
                model.EffectiveTaxRate =
                    (tax / model.AnnualIncome) * 100;
            }

            return View(model);
        }

        private decimal CalculateFederalTax(
            decimal income,
            string filingStatus)
        {
            decimal tax = 0;

            if (filingStatus == "Single")
            {
                if (income <= 11000)
                {
                    tax = income * 0.10m;
                }
                else if (income <= 44725)
                {
                    tax = 1100 +
                        ((income - 11000) * 0.12m);
                }
                else if (income <= 95375)
                {
                    tax = 5147 +
                        ((income - 44725) * 0.22m);
                }
                else
                {
                    tax = 16290 +
                        ((income - 95375) * 0.24m);
                }
            }
            else if (filingStatus == "Married Filing Jointly")
            {
                if (income <= 22000)
                {
                    tax = income * 0.10m;
                }
                else if (income <= 89450)
                {
                    tax = 2200 +
                        ((income - 22000) * 0.12m);
                }
                else if (income <= 190750)
                {
                    tax = 10294 +
                        ((income - 89450) * 0.22m);
                }
                else
                {
                    tax = 32580 +
                        ((income - 190750) * 0.24m);
                }
            }
            else if (filingStatus == "Head of Household")
            {
                if (income <= 15700)
                {
                    tax = income * 0.10m;
                }
                else if (income <= 59850)
                {
                    tax = 1570 +
                        ((income - 15700) * 0.12m);
                }
                else if (income <= 95350)
                {
                    tax = 6868 +
                        ((income - 59850) * 0.22m);
                }
                else
                {
                    tax = 14678 +
                        ((income - 95350) * 0.24m);
                }
            }

            return tax;
        }
    }
}