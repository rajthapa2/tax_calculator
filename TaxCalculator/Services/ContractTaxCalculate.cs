using TaxCalculator.Controllers;
using TaxCalculator.Helpers;

namespace TaxCalculator.Services
{
    public class ContractTaxCalculate : ICalculateTax
    {
        private readonly ITaxBracketService _taxBracketService;

        public ContractTaxCalculate(ITaxBracketService taxBracketService)
        {
            _taxBracketService = taxBracketService;
        }
        public decimal Calculate(TaxRequestDto request)
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

            var taxBrackets = _taxBracketService.GetTaxBrackets();
            return Helper.CalculateAnnualTax(yearlySalaryWithoutSuper, taxBrackets);
        }
    }
}