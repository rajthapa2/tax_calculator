using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxCalculatorService
    {
        List<TaxBracket> LoadTaxBrackets();
    }

    public class TaxCalculatorService : ITaxCalculatorService
    {
        public TaxCalculatorService()
        {
            
        }
        public List<TaxBracket> LoadTaxBrackets()
        {
            return TaxBracket.LoadTaxBrackets();
        }
    }
}
