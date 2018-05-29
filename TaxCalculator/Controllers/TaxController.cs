using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TaxCalculator.Models;

namespace TaxCalculator.Controllers
{
    [Produces("application/json")]
    [Route("api/tax")]
    public class TaxController : Controller
    {
        [HttpGet("brackets")]
        public IActionResult GetTaxBrackets()
        {
            var taxBrackets = TaxBracket.LoadTaxBrackets();
            return Ok(taxBrackets);
        }
    }
}