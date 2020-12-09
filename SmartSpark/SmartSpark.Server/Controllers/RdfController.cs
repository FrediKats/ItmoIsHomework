using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR;
using SmartSpark.Core;

namespace SmartSpark.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RdfController : ControllerBase
    {
        private readonly RdfHandler _rdfHandler;
        private readonly IHubContext<NotificationHub, INotificationClient> _context;

        public RdfController(RdfHandler rdfHandler, IHubContext<NotificationHub, INotificationClient> context)
        {
            _rdfHandler = rdfHandler;
            _context = context;
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<TripletDto>> Get()
        {
            var tripletDtos = _rdfHandler
                .GetAll()
                .Select(t => new TripletDto(t.Subject.ToString(), t.Predicate.ToString(), t.Object.ToString()))
                .ToList();
            
            return Ok(tripletDtos);
        }

        [HttpGet("create")]
        public ActionResult Create(string subject, string predicate, string obj)
        {
            _rdfHandler.Create(subject, predicate, obj);
            var tripletDto = new TripletDto(subject, predicate, predicate);
            _context.Clients.All.ReceiveNewMessage(tripletDto);
            
            return Ok();
        }
    }
}
