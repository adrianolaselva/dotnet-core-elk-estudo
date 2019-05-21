using System.Net.Http;
using Amazon;
using Amazon.SQS;
using analyst_challenge.DAO;
using analyst_challenge.DAO.Impl;
using analyst_challenge.Services;
using analyst_challenge.Services.Impl;
using analyst_challenge.Workers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Swashbuckle.AspNetCore.Swagger;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace analyst_challenge.tests
{
    public class TestContext
    {
        public HttpClient Client { get; set; }
        private TestServer _server;
        
        public TestContext()
        {
            SetupClient();
        }
        
        private void SetupClient()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            Client = _server.CreateClient();
        }
    }
}
