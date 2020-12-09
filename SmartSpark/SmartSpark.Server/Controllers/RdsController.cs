using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Linq;

namespace SmartSpark.Server.Controllers
{
    public record TripletDto(string Subject, string Predicate, string Object);

    [ApiController]
    [Route("[controller]")]
    public class RdsController : ControllerBase
    {
        private RdfHandler _rdfHandler;

        public RdsController(RdfHandler rdfHandler)
        {
            _rdfHandler = rdfHandler;
        }

        [HttpGet("get")]
        public ActionResult<IEnumerable<TripletDto>> Get()
        {
            var tripletDtos = _rdfHandler.GetAll().Select(t => new TripletDto(t.Subject.ToString(), t.Predicate.ToString(), t.Object.ToString())).ToList();
            return Ok(tripletDtos);
        }

        [HttpGet("create")]
        public void Create(string subject, string predicate, string obj)
        {
            _rdfHandler.Create(subject, predicate, obj);
        }
    }
}
