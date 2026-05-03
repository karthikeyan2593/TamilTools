using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class SalaryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double grossSalary)
        {
            // PF Calculation (12% of Basic - assumed 50% of Gross)
            double basic = grossSalary * 0.50;
            double pf = basic * 0.12;

            // Professional Tax (Tamil Nadu)
            double professionalTax = grossSalary <= 21000 ? 0 : 200;

            // TDS (Simple calculation)
            double annualSalary = grossSalary * 12;
            double tds = 0;
            if (annualSalary > 1500000)
                tds = (annualSalary - 1500000) * 0.30 / 12;
            else if (annualSalary > 1200000)
                tds = (annualSalary - 1200000) * 0.20 / 12;
            else if (annualSalary > 900000)
                tds = (annualSalary - 900000) * 0.15 / 12;
            else if (annualSalary > 600000)
                tds = (annualSalary - 600000) * 0.10 / 12;
            else if (annualSalary > 300000)
                tds = (annualSalary - 300000) * 0.05 / 12;

            double inHand = grossSalary - pf - professionalTax - tds;

            ViewBag.GrossSalary = grossSalary;
            ViewBag.Basic = Math.Round(basic, 2);
            ViewBag.PF = Math.Round(pf, 2);
            ViewBag.ProfessionalTax = Math.Round(professionalTax, 2);
            ViewBag.TDS = Math.Round(tds, 2);
            ViewBag.InHand = Math.Round(inHand, 2);
            ViewBag.InputSalary = grossSalary;

            return View("Index");
        }
    }
}