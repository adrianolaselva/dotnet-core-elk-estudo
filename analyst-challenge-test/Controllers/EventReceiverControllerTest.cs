using System.Net;
using System.Threading.Tasks;
using analyst_challenge;
using analyst_challenge_test.Helpers;
using FluentAssertions;
using Xunit;

namespace analyst_challenge_test.Controllers
{
    public class EventReceiverControllerTests : IClassFixture<Request<Startup>>
    {
        public EventReceiverControllerTests(Request<Startup> _request)
        {
            this._request = _request;
        }

        private readonly Request<Startup> _request;

        [Fact]
        public async Task GetEventReceivers404NotFoundById()
        {
            var response = await _request.Get("/v1/event-receivers/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
