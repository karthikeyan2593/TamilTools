using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class MortgageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double homePrice, double downPayment,
                                       double interestRate, int years)
        {
            double loanAmount = homePrice - downPayment;
            double monthlyRate = interestRate / (12 * 100);
            int months = years * 12;

            double monthlyPayment = loanAmount * monthlyRate *
                                    Math.Pow(1 + monthlyRate, months) /
                                    (Math.Pow(1 + monthlyRate, months) - 1);

            double totalPayment = monthlyPayment * months;
            double totalInterest = totalPayment - loanAmount;

            // Amortization
            var principalList = new List<double>();
            var interestList = new List<double>();
            var labelList = new List<string>();

            double balance = loanAmount;
            for (int i = 1; i <= months; i++)
            {
                double monthInterest = balance * monthlyRate;
                double monthPrincipal = monthlyPayment - monthInterest;
                balance -= monthPrincipal;
                principalList.Add(Math.Round(monthPrincipal, 2));
                interestList.Add(Math.Round(monthInterest, 2));
                labelList.Add("M" + i);
            }

            ViewBag.HomePrice = homePrice;
            ViewBag.DownPayment = downPayment;
            ViewBag.LoanAmount = Math.Round(loanAmount, 2);
            ViewBag.MonthlyPayment = Math.Round(monthlyPayment, 2);
            ViewBag.TotalPayment = Math.Round(totalPayment, 2);
            ViewBag.TotalInterest = Math.Round(totalInterest, 2);
            ViewBag.Years = years;
            ViewBag.PrincipalList = principalList;
            ViewBag.InterestList = interestList;
            ViewBag.LabelList = labelList;
            ViewBag.InputHomePrice = homePrice;
            ViewBag.InputDownPayment = downPayment;
            ViewBag.InputRate = interestRate;
            ViewBag.InputYears = years;

            return View("Index");
        }
    }
}