using analyst_challenge.Models;
using analyst_challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace analyst_challenge.Controllers
{
    [Route("v1/event-receivers")]
    [ApiController]
    public class EventReceiverController : ControllerBase
    {
        private readonly IEventReceiverService _eventReceiverService;
        private readonly ILogger<EventReceiverController> _logger;

        public EventReceiverController(
            ILogger<EventReceiverController> logger,
            IEventReceiverService eventReceiverService)
        {
            _logger = logger;
            _eventReceiverService = eventReceiverService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventReceiver eventReceiver)
        {
            if (eventReceiver == null)
                return BadRequest("Evento não informado");

            _eventReceiverService.Create(eventReceiver);

            return Ok(eventReceiver);
        }
    }
}
