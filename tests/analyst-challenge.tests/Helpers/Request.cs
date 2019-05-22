using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace analyst_challenge_test.Helpers
{
    public class Request<TStartup> : IDisposable where TStartup : class
    {
        private readonly HttpClient _client;
        private readonly TestServer _server;

        public Request()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<TStartup>()
                .UseConfiguration(ConfigurationSingleton.GetConfiguration());
            _server = new TestServer(webHostBuilder);
            _client = _server.CreateClient();
        }

        public void Dispose()
        {
            _client.Dispose();
            _server.Dispose();
        }

        public Task<HttpResponseMessage> Get(string url)
        {
            return _client.GetAsync(url);
        }

        public Task<HttpResponseMessage> Post<T>(string url, T body)
        {
            return _client.PostAsJsonAsync(url, body);
        }

        public Task<HttpResponseMessage> Put<T>(string url, T body)
        {
            return _client.PutAsJsonAsync(url, body);
        }

        public Task<HttpResponseMessage> Delete(string url)
        {
            return _client.DeleteAsync(url);
        }
    }
}
