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
        public ActionResult Create([FromBody] CourseTask review)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _courseTaskService.Create(review, userId);
            return Ok();
        }

        [HttpGet("{id}")]
        public ActionResult<CourseTask> Get(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return _courseTaskService.Get(id, userId);
        }

        [HttpGet("CourseTask/{courseId}")]
        public ActionResult<Review> GetSolutionReview(Guid courseId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return Ok(_courseTaskService.GetTaskInCourse(courseId, userId));
        }

        [HttpGet("Delete/{id}")]
        public void Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _courseTaskService.Delete(id, userId);
        }
    }
}