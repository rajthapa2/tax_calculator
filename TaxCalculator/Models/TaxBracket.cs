using System.Collections.Generic;

namespace TaxCalculator.Models
{
    public class TaxBracket
    {
        private static List<TaxBracket> _taxBrackets;

        public decimal MinimumThreshold { get; set; }
        public decimal MaximumThreshold { get; set; }
        public decimal Rate { get; set; }

        public static List<TaxBracket> LoadTaxBrackets()
        {
            if (_taxBrackets == null)
            {
                _taxBrackets = new List<TaxBracket>
                {
                    new TaxBracket {MinimumThreshold = 0, MaximumThreshold = 18200, Rate = 0},
                    new TaxBracket {MinimumThreshold = 18200, MaximumThreshold = 37000, Rate = 19},
                    new TaxBracket {MinimumThreshold = 37000, MaximumThreshold = 87000, Rate = 32.5m},
                    new TaxBracket {MinimumThreshold = 87000, MaximumThreshold = 180000, Rate = 37},
                    new TaxBracket {MinimumThreshold = 180000, MaximumThreshold = 1000000, Rate = 45},
                };
            }
            return _taxBrackets;
        }
    }
}
