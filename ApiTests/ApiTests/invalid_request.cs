using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace ApiTests
{
    class invalid_request : TestServerFixture
    {
        private HttpResponseMessage response;

        public invalid_request()
        {
            var json = "{\"EmploymentType\":\"Permanent\",\"DaysPerYear\":0}";
            var stringContent = new StringContent(json, Encoding.UTF8, "application/json");
            response = Client.PostAsync("api/tax/calculate", stringContent).Result;
        }

        [Test]
        public void returns_invalid_response()
        {
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public async Task gets_invalid_fields()
        {
            var content = await response.Content.ReadAsStringAsync();

            var jObjectResponse = JsonConvert.DeserializeObject<JObject>(content);

            Assert.That(jObjectResponse["Salary"], Is.Not.Null);
            Assert.That(jObjectResponse["Salary"][0].ToString(), Is.EqualTo("The Salary field is required."));
        }
    }
}
