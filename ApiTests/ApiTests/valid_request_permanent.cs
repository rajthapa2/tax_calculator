using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ApiTests
{
    [TestFixture("17000", 0)]
    [TestFixture("18200", 0)]
    [TestFixture("18201", 0.19)]
    [TestFixture("18202", 0.38)]
    [TestFixture("18203", 0.57)]
    [TestFixture("36000", 3382.00)]
    [TestFixture("50000", 7797.00)]
    [TestFixture("86977", 19814.52)]
    [TestFixture("90000", 20932.00)]
    public class valid_request_permanent : TestServerFixture
    {
        private readonly double _tax;
        private readonly HttpResponseMessage _response;

        public valid_request_permanent(string salary, double tax)
        {
            _tax = tax;
            var json = "{\"employmentType\":\"Permanent\",\"salary\":" + salary + "},\"daysPerYear\":0}";
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            _response =  Client.PostAsync("api/tax/calculate", stringContent).Result;
        }

        [Test]
        public void returns_valid_response()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task calculates_correct_tax()
        {
            var content = await _response.Content.ReadAsStringAsync();

            var jObjectResponse = JsonConvert.DeserializeObject<JObject>(content);

            Assert.That(jObjectResponse["totalTax"].Value<double>(), Is.EqualTo(_tax));
        }
    }
}
