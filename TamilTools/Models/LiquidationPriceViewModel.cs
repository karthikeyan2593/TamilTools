namespace TamilTools.Models
{
    public class LiquidationPriceViewModel
    {
        public decimal EntryPrice { get; set; }

        public decimal Margin { get; set; }

        public decimal Leverage { get; set; }

        public string PositionType { get; set; }

        public decimal LiquidationPrice { get; set; }
    }
}