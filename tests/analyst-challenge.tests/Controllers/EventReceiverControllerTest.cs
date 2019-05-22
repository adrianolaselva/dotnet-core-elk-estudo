using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using analyst_challenge;
using analyst_challenge.tests;
using analyst_challenge_test.Helpers;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace analyst_challenge_test.Controllers
{
    public class EventReceiverControllerTests : IClassFixture<Request<Startup>>
    {
        private readonly Request<Startup> _request;
        private readonly ITestOutputHelper _output;

        public EventReceiverControllerTests(Request<Startup> request, ITestOutputHelper output)
        {
            _request = request;
            _output = output;
        }
        
        [Fact]
        public async Task EventCreate_Failed()
        {
            var res = await _request.Post("/v1/event-receivers", new
            {
                timestamp = 1539112021301,
                tag = "brasil.sudeste.sensor01",
                valor = "912391232193912",
            });
            
            res.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }
        
        [Fact]
        public async Task EventCreate_OK()
        {
            var res = await _request.Post("/v1/event-receivers", new
            {
                timestamp = 1539112021,
                tag = "brasil.sudeste.sensor01",
                valor = "912391232193912",
            });
            
            res.StatusCode.Should().Be(HttpStatusCode.OK);
        }
        
        [Fact]
        public async Task EventFindById_NotFound()
        {
            var res = await _request.Get("/v1/event-receivers/36425b28-250b-40c2-83a5-1ce657cfe358");
            
            res.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
        
    }
}
