using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using analyst_challenge.DAO;
using Microsoft.AspNetCore.Mvc;
using analyst_challenge.Models;
using analyst_challenge.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace analyst_challenge.Controllers
{
    [Route("v1/event-receivers")]
    [ApiController]
    public class EventReceiverController : ControllerBase
    {
        private readonly ILogger<EventReceiverController> _logger;
        private readonly IEventReceiverService _eventReceiverService;

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
