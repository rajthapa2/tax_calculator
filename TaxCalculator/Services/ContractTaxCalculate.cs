using TaxCalculator.Controllers;

namespace TaxCalculator.Services
{
    public class ContractTaxCalculate : CalculateTax
    {
        public override decimal Calculate(TaxRequestDto request)
        {
            decimal yearlySalaryWithoutSuper;

            if (request.IncludesSuper.HasValue && request.IncludesSuper == true)
            {
                // x + 9.5 % of x = salary
                // x + 0.095x = salary
                // 1.095x = salary
                // x = salary / 1.095
                var dayRateWithoutSuper = request.Salary.Value / 1.095m;
                yearlySalaryWithoutSuper = dayRateWithoutSuper * request.DaysPerYear.Value;
            }
            else
            {
                yearlySalaryWithoutSuper = request.DaysPerYear.Value * request.Salary.Value;
            }

            return FullTimeTaxCalculate.CalculateAnnualyTax(yearlySalaryWithoutSuper);
        }
    }
}