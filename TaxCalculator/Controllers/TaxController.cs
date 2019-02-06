using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    [Produces("application/json")]
    [Route("api/tax")]
    public class TaxController : Controller
    {
        private readonly ITaxCalculatorService _taxCalculatorService;
        private readonly ITaxBracketService _taxBracketService;

        public TaxController(ITaxCalculatorService taxCalculatorService, ITaxBracketService taxBracketService)
        {
            _taxCalculatorService = taxCalculatorService;
            _taxBracketService = taxBracketService;
        }

        [HttpGet("brackets")]
        public IActionResult GetTaxBrackets()
        {
            var taxBrackets = _taxBracketService.GetTaxBrackets();
            return Ok(taxBrackets);
        }

        [HttpPost("calculate")]
        public IActionResult CalculateTax([FromBody] TaxRequestDto taxRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var taxResponse = _taxCalculatorService.Calculate(taxRequest);
            return Ok(taxResponse);
        }
    }
}