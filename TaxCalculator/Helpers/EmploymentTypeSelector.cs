using TaxCalculator.Controllers;
using TaxCalculator.Services;

namespace TaxCalculator.Helpers
{
    public static class EmploymentTypeSelector
    {
        public static ICalculateTax Get(EmploymentType employmentType)
        {
            return employmentType == EmploymentType.Contract ? (ICalculateTax) new ContractTaxCalculate(new TaxBracketService()) : new FullTimeTaxCalculate(new TaxBracketService());
        }
    }
}