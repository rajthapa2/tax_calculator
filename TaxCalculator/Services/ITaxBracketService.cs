using System.Collections.Generic;
using TaxCalculator.Models;

namespace TaxCalculator.Services
{
    public interface ITaxBracketService
    {
        List<TaxBracket> GetTaxBrackets();
    }
}