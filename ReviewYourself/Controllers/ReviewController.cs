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
    public class ReviewController : ApiController
    {
        // POST: Add/{token}
        [HttpPost]
        [Route("Add/{token}")]
        public void Add([FromUri]Token token, [FromBody]Review review)
        {
            throw new NotImplementedException();
        }

        // GET: Get/{token}/{reviewId}
        [HttpGet]
        [Route("Get/{token}/{reviewId}")]
        public Review Get([FromUri]Token token, [FromUri]Guid reviewId)
        {
            throw new NotImplementedException();
        }

        // GET: GetBySolution/{token}/{solutionId}
        [HttpGet]
        [Route("GetBySolution/{token}/{solutionId}")]
        public IEnumerable<Review> GetBySolution([FromUri]Token token, [FromUri]Guid solutionId)
        {
            throw new NotImplementedException();
        }

        // DELETE: Delete/{token}/{reviewId}
        [HttpDelete]
        [Route("Delete/{token}/{reviewId}")]
        public void Delete([FromUri]Guid token, [FromUri]Guid reviewId)
        {
            throw new NotImplementedException();
        }
    }
}
