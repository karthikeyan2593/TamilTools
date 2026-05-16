namespace TamilTools.Models
{
    public class FederalTaxViewModel
    {
        public decimal AnnualIncome { get; set; }

        public string FilingStatus { get; set; } = "Single";

        public decimal FederalTax { get; set; }

        public decimal NetIncome { get; set; }

        public decimal EffectiveTaxRate { get; set; }
    }
}