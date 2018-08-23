using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(Course))]
        public Course Create([FromBody]Course course,[FromRoute]Token token)
        {
            return _courseService.CreateCourse(course, token.UserId);
        }

        [HttpGet("id")]
        [ProducesResponseType(200, Type = typeof(Course))]
        public Course Get(Guid id)
        {
            return _courseService.GetCourse(id);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(Course))]
        public ActionResult<ICollection<Course>> Find([FromRoute]string courseName)
        {
            return Ok(_courseService.FindCourses(courseName));
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(void))]
        public void Update([FromBody] Course course, [FromRoute]Token token)
        {
            _courseService.UpdateCourse(course, token.UserId);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(200, Type = typeof(void))]
        public void Delete(Guid id, [FromRoute]Token token)
        {
            _courseService.DeleteCourse(id, token.UserId);
        }
    }
}
