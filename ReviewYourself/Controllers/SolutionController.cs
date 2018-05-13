using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;

namespace ReviewYourself.Controllers
{
    public class SolutionController : ApiController
    {
        // POST: Add/{token}
        [HttpPost]
        [Route("Add/{token}")]
        public void Add([FromUri]Token token, [FromBody]Solution solution)
        {
            throw new NotImplementedException();
        }

        // GET: Get/{token}/{solutionId}
        [HttpGet]
        [Route("Get/{token}/{solutionId}")]
        public Solution Get([FromUri]Token token, [FromUri]Guid solutionId)
        {
            throw new NotImplementedException();
        }

        // GET: GetByTask/{token}/{taskId}
        [HttpGet]
        [Route("GetByTask/{token}/{taskId}")]
        public IEnumerable<Solution> GetByTask([FromUri]Token token, [FromUri]Guid taskId)
        {
            throw new NotImplementedException();
        }

        // DELETE: Delete/{token}/{solutionId}
        [HttpDelete]
        [Route("Delete/{token}/{solutionId}")]
        public void Delete([FromUri]Guid token, [FromUri]Guid solutionId)
        {
            throw new NotImplementedException();
        }

        // POST: Is-can-review/{token}/{solutionId}
        [HttpPost]
        [Route("Is-can-review/{token}/{solutionId}")]
        public bool IsCanReview([FromUri]Token token, [FromUri]Guid solutionId)
        {
            throw new NotImplementedException();
        }

        // POST: Resolve-solution/{token}/{solutionId}
        [HttpPost]
        [Route("Resolve-solution/{token}/{solutionId}")]
        public void ResolveSolution([FromUri]Token token, [FromUri]Guid solutionId, [FromBody]Review review)
        {
            throw new NotImplementedException();
        }
    }
}
