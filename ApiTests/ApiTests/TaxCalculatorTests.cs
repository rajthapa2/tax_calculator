using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//using NUnit.Framework;
//using Assert = NUnit.Framework.Assert;

namespace ApiTests
{
    [TestClass]
    public class TaxCalculator : TestServerFixture
    {
        [TestMethod]
        public async Task load_tax_brackets()
        {
            var response = await Client.GetAsync("/brackets");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            Assert.AreEqual(content, "");
        }

    }
}
