using TaxCalculator.Controllers;

namespace TaxCalculator.Services
{
    public class FullTimeTaxCalculate : CalculateTax
    {
        public override decimal Calculate(TaxRequestDto request)
        {
            var currentSalary = request.Salary;

          return  CalculateAnnualyTax(currentSalary.Value);
        }

        public static decimal CalculateAnnualyTax(decimal salary)
        {
            var totaltax = 0m;

            foreach (var taxBracket in TaxBrackets)
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
                    totaltax += taxBracket.Rate / 100 * totalTaxableSalaryInThisBracket;
                }
                else
                {
                    break;
                }
            }
            return totaltax;
        }
    }
}