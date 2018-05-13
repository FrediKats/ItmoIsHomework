using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/reviews")]
    public class ReviewController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public void Add([FromUri]Token token, [FromBody]Review review)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        [Route("Get/{reviewId}")]
        public Review Get(Guid reviewId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetBySolution/{solutionId}")]
        public IEnumerable<Review> GetBySolution(Guid solutionId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete/{reviewId}")]
        public void Delete(Guid reviewId, [FromUri]Guid token)
        {
            throw new NotImplementedException();
        }
    }
}
