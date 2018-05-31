using System;
using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using TaxCalculator;

namespace ApiTests
{
    public class TestServerFixture : IDisposable
    {
        public readonly TestServer _testServer;
        public HttpClient Client { get; set; }
        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();
            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();
        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}