using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/CourseSolution")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] CourseSolution review, [FromRoute] Token token)
        {
            _solutionService.Create(review, token.UserId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseSolution> Get(Guid id, [FromRoute] Token token)
        {
            return _solutionService.Get(id, token.UserId);
        }

        [HttpGet("UserSolution/{solutionId}/{userId}")]
        public ActionResult<CourseSolution> GetUserSolution(Guid taskId, Guid userId, [FromRoute] Token token)
        {
            return _solutionService.GetUserSolution(taskId, userId, token.UserId);
        }

        [HttpGet("TaskSolutions/{solutionId}")]
        public ActionResult<Review> GetSolutionReview(Guid taskId, [FromRoute] Token token)
        {
            return Ok(_solutionService.GetSolutionsByTask(taskId, token.UserId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id, [FromRoute] Token token)
        {
            _solutionService.Delete(id, token.UserId);
        }
    }
}