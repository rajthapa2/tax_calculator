using TaxCalculator.Controllers;
using TaxCalculator.Helpers;

namespace TaxCalculator.Services
{
    public class FullTimeTaxCalculate : ICalculateTax
    {
        private readonly ITaxBracketService _taxBracketService;

        public FullTimeTaxCalculate(ITaxBracketService taxBracketService)
        {
            _taxBracketService = taxBracketService;
        }
        public decimal Calculate(TaxRequestDto request)
        {
            var currentSalary = request.Salary;

          return  CalculateAnnualTax(currentSalary.Value);
        }

        public decimal CalculateAnnualTax(decimal salary)
        {
            var taxBrackets = _taxBracketService.GetTaxBrackets();
            return Helper.CalculateAnnualTax(salary, taxBrackets);
        }
    }
}