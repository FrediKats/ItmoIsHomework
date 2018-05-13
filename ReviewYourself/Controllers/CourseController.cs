using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{ 
    [RoutePrefix("api/courses")]
    public class CourseController : ApiController
    {
        private readonly ICourseService _courseService;
        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpPost]
        public void Add([FromUri]Token token, [FromBody]Course course)
        {
            _courseService.AddCourse(course, token);
        }

        [HttpPost]
        [Route("Invite/{courseId}/{username}")]
        public void InviteUser(Guid courseId, string username, [FromUri]Token token)
        {
            _courseService.InviteUser(username, courseId, token);
        }

        [HttpPost]
        [Route("Accept-Invite/{courseId}")]
        public void AcceptInvite(Guid courseId, [FromUri]Token token)
        {
            _courseService.AcceptInvite(courseId, token);
        }

        [HttpGet]
        [Route("Get/{courseId}")]
        public Course Get(Guid courseId, [FromUri]Token token)
        {
            return _courseService.GetCourse(courseId, token);
        }

        [HttpGet]
        [Route("GetByUser/{userId}")]
        public IEnumerable<Course> GetByUser(Guid userId, [FromUri]Token token)
        {
            return _courseService.GetCourseCollectionByUser(userId, token);
        }

        [HttpPost]
        [Route("Update")]
        public void Update([FromUri]Token token, [FromBody]Course course)
        {
            _courseService.UpdateCourse(course, token);
        }

        [HttpDelete]
        [Route("{courseId}")]
        public void Delete(Guid courseId, [FromUri]Token token)
        {
            _courseService.DeleteCourse(courseId, token);
        }

        [HttpDelete]
        [Route("Delete-member/{courseId}/{userId}")]
        public void DeleteMember(Guid courseId, Guid userId,[FromUri]Token token)
        {
            _courseService.DeleteMember(courseId, userId, token);
        }
    }
}
