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
    public class valid_request_permanent : TestServerFixture
    {
        private readonly double _tax;
        private HttpResponseMessage response;

        public valid_request_permanent(string salary, double tax)
        {
            _tax = tax;
            var json = "{\"EmploymentType\":\"Permanent\",\"Salary\":" + salary + "},\"Unit\":0}";
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            response =  Client.PostAsync("api/tax/calculate", stringContent).Result;
        }

        [Test]
        public void returns_valid_response()
        {
            response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task calculates_correct_tax()
        {
            var content = await response.Content.ReadAsStringAsync();

            var jObjectResponse = JsonConvert.DeserializeObject<JObject>(content);

            Assert.That(jObjectResponse["totalTax"].Value<double>(), Is.EqualTo(_tax));
        }
    }
}
