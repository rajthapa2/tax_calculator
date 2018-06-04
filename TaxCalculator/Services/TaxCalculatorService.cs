using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp.Syntax;
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
                if (currentSalary >= taxBracket.MaximumThreshold)
                {
                    var totalTaxableSalaryInThisBracket = taxBracket.MaximumThreshold - taxBracket.MinimumThreshold;

                    totaltax += taxBracket.Rate / 100 * totalTaxableSalaryInThisBracket;
                    continue;
                }
                else
                {
                    var totalTaxableSalaryInThisBracket = currentSalary.Value - taxBracket.MinimumThreshold;
                    totaltax += taxBracket.Rate / 100 * totalTaxableSalaryInThisBracket;

                    break;
                }
            }
            return new TaxResponse { TotalTax = decimal.Round(totaltax, 2) };
        }
    }
}
