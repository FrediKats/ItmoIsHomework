using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/Course")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost("Create")]
        public ActionResult<Course> Create([FromBody] Course course)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            return _courseService.Create(course, userId);
        }

        [HttpGet("id")]
        public ActionResult<Course> Get(Guid id)
        {
            return _courseService.Get(id);
        }

        [HttpGet("Find")]
        public ActionResult<ICollection<Course>> Find([FromRoute] string courseName)
        {
            return Ok(_courseService.FindCourses(courseName));
        }

        [HttpPost("Update")]
        public ActionResult Update([FromBody] Course course)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _courseService.Update(course, userId);
            return Ok();
        }

        [HttpGet("Delete/{id}")]
        public ActionResult Delete(Guid id)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _courseService.Delete(id, userId);
            return Ok();
        }
    }
}