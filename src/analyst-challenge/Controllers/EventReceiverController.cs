using analyst_challenge.DTO;
using analyst_challenge.Enums;
using analyst_challenge.Models;
using analyst_challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nest;

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
        
        [HttpGet("{uuid}")]
        public IActionResult FindById([FromRoute] string uuid)
        {
            
            var result = _eventReceiverService.FindById(uuid);

            if (result == null)
                return NotFound();
            
            return Ok(result);
        }
        
        [HttpGet]
        public IActionResult List( 
            [FromQuery] PaginationQueryParameters paginationQueryParameters,
            [FromQuery(Name = "tag")] string tag)
        {
            
            var events = _eventReceiverService.List(
                paginationQueryParameters.From, 
                paginationQueryParameters.Size, 
                tag
            );

            return Ok(events);
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
