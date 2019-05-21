using System;
using System.Net;
using System.Threading.Tasks;
using analyst_challenge;
using analyst_challenge.tests;
using analyst_challenge_test.Helpers;
using FluentAssertions;
using Xunit;

namespace analyst_challenge_test.Controllers
{
    public class EventReceiverControllerTests : IClassFixture<Request<Startup>>, IDisposable
    {
        private readonly TestContext _testContext;
        
        public EventReceiverControllerTests()
        {
            _testContext = new TestContext();
        }
        
        [Fact]
        public async Task GetEventReceivers404NotFoundById()
        {
            var response = await _testContext.Client.GetAsync("/v1/event-receivers/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public void Dispose()
        {
            
        }
    }
}
