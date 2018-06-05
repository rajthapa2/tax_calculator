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

        public List<CalculateTax> CalculateTaxs = new List<CalculateTax>();
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
            var employmentType = taxRequest.EmploymentType;

            decimal totalTax = 0;

            switch (employmentType)
            {
                case EmploymentType.Permanent:
                {
                    var fullTimeTaxCalculate = new FullTimeTaxCalculate();
                    totalTax = fullTimeTaxCalculate.Calculate(taxRequest);
                    break;
                }
                case EmploymentType.Contract:
                {
                    var contractTaxCalculate = new ContractTaxCalculate();
                    totalTax = contractTaxCalculate.Calculate(taxRequest);
                    break;
                }
            }
            return new TaxResponse {TotalTax = decimal.Round(totalTax, 2)};
        }
    }

    public abstract class CalculateTax
    {
        public List<TaxBracket> TaxBrackets { get; set; }

        protected CalculateTax()
        {
            TaxBrackets = TaxBracket.LoadTaxBrackets();
        }
        public virtual decimal Calculate(TaxRequestDto request)
        {
            return 0;
        }
    }

    public class FullTimeTaxCalculate : CalculateTax
    {
        public override decimal Calculate(TaxRequestDto request)
        {
            var currentSalary = request.Salary;

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
            return totaltax;
        }
    }

    public class ContractTaxCalculate : CalculateTax
    {
        public override decimal Calculate(TaxRequestDto request)
        {
            throw new System.NotImplementedException();
        }
    }
}
