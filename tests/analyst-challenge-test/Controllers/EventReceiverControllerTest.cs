using analyst_challenge;
using analyst_challenge_test.Helpers;

namespace analyst_challenge_test.Controllers
{
    public class EventReceiverControllerTests : IClassFixture<Request<Startup>>
    {
        private readonly Request<Startup> _request;

        public EventReceiverControllerTests(Request<Startup> _request)
        {
            this._request = _request;
        }

        [Fact]
        public async Task GetEventReceivers404NotFoundById()
        {
            var response = await _request.Get("/v1/event-receivers/1");
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }
    }
}
