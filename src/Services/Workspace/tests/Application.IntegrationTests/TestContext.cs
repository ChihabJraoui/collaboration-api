using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Web;

namespace Application.IntegrationTests
{
    public class TestContext
    {
        private TestServer _server;
        public HttpClient Client { get; private set; }

        public TestContext()
        {
            SetUpClient();
            Dispose();
        }

        private void SetUpClient()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>());

            Client = _server.CreateClient();
        }

        public virtual void Dispose()
        {
            Client.Dispose();
            _server.Dispose();
        }
    }
}
