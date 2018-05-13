using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/solutions")]
    public class SolutionController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public void Add([FromUri]Token token, [FromBody]Solution solution)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get/{solutionId}")]
        public Solution Get(Guid solutionId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetByTask/{taskId}")]
        public IEnumerable<Solution> GetByTask(Guid taskId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete/{solutionId}")]
        public void Delete(Guid solutionId, [FromUri]Guid token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Is-can-review/{solutionId}")]
        public bool IsCanReview(Guid solutionId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Resolve-solution/{solutionId}")]
        public void ResolveSolution(Guid solutionId, Review review, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }
    }
}
