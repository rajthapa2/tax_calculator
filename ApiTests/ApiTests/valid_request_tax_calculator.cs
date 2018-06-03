using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ApiTests
{
    public class valid_request_tax_calculator : TestServerFixture
    {
        private HttpResponseMessage response;

        public valid_request_tax_calculator()
        {
            var json = "{\"EmploymentType\":\"Permanent\",\"Salary\":36000,\"Unit\":0}";
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

            Assert.That(jObjectResponse["totalTax"].Value<decimal>(), Is.EqualTo(3657.00));
        }
    }
}
