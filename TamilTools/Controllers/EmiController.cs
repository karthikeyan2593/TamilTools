using Microsoft.AspNetCore.Mvc;

namespace TamilTools.Controllers
{
    public class EmiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(double loanAmount, double interestRate, int months)
        {
            double monthlyRate = interestRate / (12 * 100);
            double emi = loanAmount * monthlyRate *
                         Math.Pow(1 + monthlyRate, months) /
                         (Math.Pow(1 + monthlyRate, months) - 1);

            double totalAmount = emi * months;
            double totalInterest = totalAmount - loanAmount;

            var principalList = new List<double>();
            var interestList = new List<double>();
            var labelList = new List<string>();

            double balance = loanAmount;
            for (int i = 1; i <= months; i++)
            {
                double monthInterest = balance * monthlyRate;
                double monthPrincipal = emi - monthInterest;
                balance -= monthPrincipal;

                principalList.Add(Math.Round(monthPrincipal, 2));
                interestList.Add(Math.Round(monthInterest, 2));
                labelList.Add("M" + i);
            }

            ViewBag.EMI = Math.Round(emi, 2);
            ViewBag.TotalAmount = Math.Round(totalAmount, 2);
            ViewBag.TotalInterest = Math.Round(totalInterest, 2);
            ViewBag.LoanAmount = loanAmount;
            ViewBag.Months = months;
            ViewBag.PrincipalList = principalList;
            ViewBag.InterestList = interestList;
            ViewBag.LabelList = labelList;

            // Input values retain பண்ண
            ViewBag.InputLoan = loanAmount;
            ViewBag.InputRate = interestRate;
            ViewBag.InputMonths = months;

            return View("Index");
        }
    }
}