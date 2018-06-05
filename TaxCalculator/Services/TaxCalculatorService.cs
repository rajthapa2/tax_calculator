using System.Collections.Generic;
using System.Linq;
using TaxCalculator.Controllers;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxCalculatorService
    {
        List<TaxBracket> LoadTaxBrackets();
        TaxResponse Calculate(TaxRequestDto taxRequest);
    }

    public class TaxCalculatorService : ITaxCalculatorService
    {
        public static List<TaxBracket> TaxBrackets { get; set; }
        public TaxCalculatorService()
        {
            TaxBrackets = LoadTaxBrackets();
        }
        public List<TaxBracket> LoadTaxBrackets()
        {
            if (TaxBrackets != null && TaxBrackets.Any())
            {
                return TaxBrackets;
            }
            return TaxBracket.LoadTaxBrackets();
        }

        public TaxResponse Calculate(TaxRequestDto taxRequest)
        {
            var currentSalary = taxRequest.Salary;

            var totaltax = 0m;

            foreach (var taxBracket in TaxBrackets)
            {
                if (currentSalary >= taxBracket.MinimumThreshold)
                {
                    decimal totalTaxableSalaryInThisBracket;
                    if (currentSalary >= taxBracket.MaximumThreshold)
                    {
                        totalTaxableSalaryInThisBracket = taxBracket.MaximumThreshold - taxBracket.MinimumThreshold;
                    }
                    else
                    {
                        totalTaxableSalaryInThisBracket = currentSalary.Value - taxBracket.MinimumThreshold;
                    }
                    totaltax += taxBracket.Rate / 100 * totalTaxableSalaryInThisBracket;
                }
                else
                {
                    break;
                }
            }
            return new TaxResponse { TotalTax = decimal.Round(totaltax, 2) };
        }
    }
}
