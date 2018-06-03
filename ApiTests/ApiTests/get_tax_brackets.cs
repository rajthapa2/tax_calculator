using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Newtonsoft.Json;
using NUnit.Framework;
using TaxCalculator.Controllers;
using TaxCalculator.Models;

namespace ApiTests
{
    public class TaxCalculator  : TestServerFixture
    {
        [Test]
        public async Task get_tax_brackets()
        {
            var response = await Client.GetAsync("api/tax/brackets");
            response.EnsureSuccessStatusCode();

            Assert.That(response.Content.Headers.ContentType.MediaType, Is.EqualTo("application/json"));

            var content = await response.Content.ReadAsStringAsync();

            var taxBrackets = JsonConvert.DeserializeObject<List<TaxBracket>>(content);
            Assert.That(taxBrackets.Count, Is.EqualTo(2));
        }
    }
}
