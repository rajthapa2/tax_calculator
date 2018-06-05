using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;
using TaxCalculator.Controllers;

namespace ApiTests
{
    [TestFixture(400, 0)]
    [TestFixture(18200.0, 0)]
    [TestFixture(18201.0, 0.19)]
    [TestFixture(18202.0, 0.38)]
    [TestFixture(18203.0, 0.57)]
    [TestFixture(36000.0, 3382.00)]
    [TestFixture(50000.0, 7797.00)]
    [TestFixture(90000.0, 20932.00)]
    public class valid_request_contract : TestServerFixture
    {
        private readonly double _tax;
        private readonly HttpResponseMessage _response;

        public valid_request_contract(double salary, double tax)
        {
            _tax = tax;

            var taxRequest = new TaxRequestDto
            {
                DaysPerYear = 240,
                EmploymentType = EmploymentType.Contract,
                Salary = (decimal) salary
            };

            var json = JsonConvert.SerializeObject(taxRequest, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            _response =  Client.PostAsync("api/tax/calculate", stringContent).Result;
        }

        [Test]
        public void returns_valid_response()
        {
            _response.EnsureSuccessStatusCode();
        }

        [Test]
        public async Task contract_calculates_correct_tax()
        {
            var content = await _response.Content.ReadAsStringAsync();

            var jObjectResponse = JsonConvert.DeserializeObject<JObject>(content);

            Assert.That(jObjectResponse["totalTax"].Value<double>(), Is.EqualTo(_tax));
        }
    }
}
