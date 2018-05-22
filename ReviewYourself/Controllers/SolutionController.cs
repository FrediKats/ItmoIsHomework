using System;
using System.Web.Http;
using System.Web.Http.Description;
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
        [ResponseType(typeof(void))]
        public IHttpActionResult Add([FromUri] Token token, [FromBody] Solution solution)
        {
            try
            {
                _solutionService.CreateSolution(token, solution);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Solution))]
        [Route("{solutionId}")]
        public IHttpActionResult Get(Guid solutionId, [FromUri] Token token)
        {
            try
            {
                var result = _solutionService.GetSolution(token, solutionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Solution[]))]
        [Route("GetByTask/{taskId}")]
        public IHttpActionResult GetByTask(Guid taskId, [FromUri] Token token)
        {
            try
            {
                var result = _solutionService.GetSolutionListByTask(token, taskId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Solution))]
        [Route("GetByTaskAndUser/{taskId}/{userId}")]
        public IHttpActionResult GetByTaskAndUser(Guid taskId, Guid userId, [FromUri] Token token)
        {
            try
            {
                var result = _solutionService.GetSolutionByTaskAndUser(token, taskId, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpDelete]
        [ResponseType(typeof(void))]
        [Route("{solutionId}")]
        public IHttpActionResult Delete(Guid solutionId, [FromUri] Token token)
        {
            try
            {
                _solutionService.DeleteSolution(token, solutionId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(bool))]
        [Route("Is-can-review/{solutionId}")]
        public IHttpActionResult IsCanReview(Guid solutionId, [FromUri] Token token)
        {
            try
            {
                var result = _solutionService.IsCanAddReview(token, solutionId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Resolve-solution/{solutionId}")]
        public IHttpActionResult ResolveSolution(Guid solutionId, Review review, [FromUri] Token token)
        {
            try
            {
                _solutionService.ResolveSolution(token, solutionId, review);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}