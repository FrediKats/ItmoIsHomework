using System;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/CourseTask")]
    [ApiController]
    public class CourseTaskController : ControllerBase
    {
        private readonly ICourseTaskService _courseTaskService;

        public CourseTaskController(ICourseTaskService courseTaskService)
        {
            _courseTaskService = courseTaskService;
        }

        [HttpPost("Create")]
        public ActionResult Create([FromBody] CourseTask review, [FromRoute] Token token)
        {
            _courseTaskService.Create(review, token.UserId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseTask> Get(Guid id, [FromRoute] Token token)
        {
            return _courseTaskService.Get(id, token.UserId);
        }

        [HttpGet("CourseTask/{courseId}")]
        public ActionResult<Review> GetSolutionReview(Guid courseId, [FromRoute] Token token)
        {
            return Ok(_courseTaskService.GetTaskInCourse(courseId, token.UserId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id, [FromRoute] Token token)
        {
            _courseTaskService.Delete(id, token.UserId);
        }
    }
}