namespace TamilTools.Models
{
    public class FuturesPnLViewModel
    {
        public decimal BuyPrice { get; set; }

        public decimal SellPrice { get; set; }

        public decimal InvestmentAmount { get; set; }

        public decimal TradingFee { get; set; }

        public decimal FinalAmount { get; set; }

        public decimal ProfitLoss { get; set; }

        public decimal ROI { get; set; }
    }
}