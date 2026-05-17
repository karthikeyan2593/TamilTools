// Models/CryptoPositionSizeViewModel.cs

namespace TamilTools.Models
{
    public class CryptoPositionSizeViewModel
    {
        public decimal AccountBalance { get; set; }

        public decimal RiskPercent { get; set; }

        public decimal EntryPrice { get; set; }

        public decimal StopLossPrice { get; set; }

        public decimal Leverage { get; set; }

        public decimal RiskAmount { get; set; }

        public decimal PositionSize { get; set; }

        public decimal Quantity { get; set; }

        public decimal MaxLoss { get; set; }
    }
}