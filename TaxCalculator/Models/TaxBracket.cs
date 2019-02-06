namespace TaxCalculator.Models
{
    public class TaxBracket
    {
        public decimal MinimumThreshold { get; set; }
        public decimal MaximumThreshold { get; set; }
        public decimal Rate { get; set; }
    }
}
