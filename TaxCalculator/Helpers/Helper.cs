using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.Helpers
{
    public static class Helper
    {
        public static decimal CalculateAnnualTax(decimal salary, List<TaxBracket> taxBrackets)
        {
            var total = 0m;
            foreach (var taxBracket in taxBrackets)
            {
                if (salary >= taxBracket.MinimumThreshold)
                {
                    decimal totalTaxableSalaryInThisBracket;
                    if (salary >= taxBracket.MaximumThreshold)
                    {
                        totalTaxableSalaryInThisBracket = taxBracket.MaximumThreshold - taxBracket.MinimumThreshold;
                    }
                    else
                    {
                        totalTaxableSalaryInThisBracket = salary - taxBracket.MinimumThreshold;
                    }
                    total += taxBracket.Rate / 100 * totalTaxableSalaryInThisBracket;
                }
                else
                {
                    break;
                }
            }
            return total;
        }
    }
}