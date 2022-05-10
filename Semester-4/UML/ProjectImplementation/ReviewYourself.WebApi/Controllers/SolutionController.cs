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
        public ActionResult Create([FromBody] CourseSolution review)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _solutionService.Create(review, userId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseSolution> Get(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return _solutionService.Get(id, userId);
        }

        [HttpGet("UserSolution/{solutionId}/{userId}")]
        public ActionResult<CourseSolution> GetUserSolution(Guid taskId, Guid userId)
        {
            return _solutionService.GetUserSolution(taskId, userId, userId);
        }

        [HttpGet("TaskSolutions/{solutionId}")]
        public ActionResult<Review> GetSolutionReview(Guid taskId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return Ok(_solutionService.GetSolutionsByTask(taskId, userId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _solutionService.Delete(id, userId);
        }
    }
}