using TaxCalculator.Controllers;
using TaxCalculator.Helpers;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxCalculatorService
    {
        TaxResponse Calculate(TaxRequestDto taxRequest);
    }

    public class TaxCalculatorService : ITaxCalculatorService
    {
        private readonly ITaxBracketService _taxBracketService;
        public TaxCalculatorService(ITaxBracketService taxBracketService)
        {
            _taxBracketService = taxBracketService;
        }

        public TaxResponse Calculate(TaxRequestDto taxRequest)
        {
            var employmentType = taxRequest.EmploymentType;
            var taxService = EmploymentTypeSelector.Get(employmentType);
            var totalTax = taxService.Calculate(taxRequest);

            return new TaxResponse {TotalTax = decimal.Round(totalTax, 2)};
        }
    }

    public interface ICalculateTax
    {
        decimal Calculate(TaxRequestDto request);
    }
}
