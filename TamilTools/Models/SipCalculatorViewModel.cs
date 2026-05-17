// Models/SipCalculatorViewModel.cs

namespace TamilTools.Models
{
    public class SipCalculatorViewModel
    {
        public decimal MonthlyInvestment { get; set; }

        public decimal ExpectedReturnRate { get; set; }

        public int Years { get; set; }

        public decimal InvestedAmount { get; set; }

        public decimal EstimatedReturns { get; set; }

        public decimal TotalValue { get; set; }
    }
}