using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/Solution")]
    [ApiController]
    public class SolutionController : ControllerBase
    {
        private readonly ISolutionService _solutionService;

        public SolutionController(ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] Solution review, [FromRoute] Token token)
        {
            _solutionService.Create(review, token.UserId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<Solution> Get(Guid id, [FromRoute] Token token)
        {
            return _solutionService.Get(id, token.UserId);
        }

        [HttpGet("UserSolution/{solutionId}/{userId}")]
        public ActionResult<Solution> GetUserSolution(Guid taskId, Guid userId, [FromRoute] Token token)
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