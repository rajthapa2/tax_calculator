using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;
using TaxCalculator.Services;

namespace TaxCalculator.Controllers
{
    [Produces("application/json")]
    [Route("api/tax")]
    public class TaxController : Controller
    {
        private ITaxCalculatorService _taxCalculatorService;
        public List<TaxBracket> TaxBrackets { get; set; }

        public TaxController(ITaxCalculatorService taxCalculatorService)
        {
            _taxCalculatorService = taxCalculatorService;
            TaxBrackets = taxCalculatorService.LoadTaxBrackets();
        }

        [HttpGet("brackets")]
        public IActionResult GetTaxBrackets()
        {
            return Ok(TaxBrackets);
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