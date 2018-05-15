using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/solutions")]
    public class SolutionController : ApiController
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost]
        public void Add([FromUri]Token token, [FromBody]Solution solution)
        {
            _solutionService.CreateSolution(solution, token);
        }

        [HttpGet]
        [Route("{solutionId}")]
        public Solution Get(Guid solutionId, [FromUri]Token token)
        {
            return _solutionService.GetSolution(solutionId, token);
        }

        [HttpGet]
        [Route("GetByTask/{taskId}")]
        public IEnumerable<Solution> GetByTask(Guid taskId, [FromUri]Token token)
        {
            return _solutionService.GetSolutionByTask(taskId, token);
        }

        [HttpGet]
        [Route("GetByTask/{taskId}/{userId")]
        public Solution GetByTaskAndUser(Guid taskId, Guid userId, [FromUri]Token token)
        {
            return _solutionService.GetSolutionByTaskAndUser(taskId, userId, token);
        }


        [HttpDelete]
        [Route("{solutionId}")]
        public void Delete(Guid solutionId, [FromUri]Token token)
        {
            _solutionService.DeleteSolution(solutionId, token);
        }

        [HttpPost]
        [Route("Is-can-review/{solutionId}")]
        public bool IsCanReview(Guid solutionId, [FromUri]Token token)
        {
            return _solutionService.IsCanAddReview(solutionId, token);
        }

        [HttpPost]
        [Route("Resolve-solution/{solutionId}")]
        public void ResolveSolution(Guid solutionId, Review review, [FromUri]Token token)
        {
            _solutionService.ResolveSolution(solutionId, review, token);
        }
    }
}
