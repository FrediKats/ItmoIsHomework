using System;
using System.Collections.Generic;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;

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
        public void Create([FromUri] Token token, [FromBody] Course course)
        {
            _courseService.AddCourse(token, course);
        }

        [HttpPost]
        [Route("Invite/{courseId}/{username}")]
        public void InviteUser(Guid courseId, string username, [FromUri] Token token)
        {
            _courseService.InviteUser(token, username, courseId);
        }

        [HttpPost]
        [Route("Accept-Invite/{courseId}")]
        public void AcceptInvite(Guid courseId, [FromUri] Token token)
        {
            _courseService.AcceptInvite(token, courseId);
        }

        [HttpGet]
        [Route("IsMember")]
        public bool IsMember([FromUri] Token token)
        {
            return _courseService.IsMember(token);
        }


        [HttpGet]
        [Route("GetById/{courseId}")]
        public Course GetById(Guid courseId, [FromUri] Token token)
        {
            return _courseService.GetCourse(token, courseId);
        }

        [HttpGet]
        [Route("GetByUser")]
        public IEnumerable<Course> GetByUser([FromUri] Token token)
        {
            return _courseService.GetCourseCollectionByUser(token, token.UserId);
        }

        [HttpGet]
        [Route("GetByUser/{userId}")]
        public IEnumerable<Course> GetByUser(Guid userId, [FromUri] Token token)
        {
            return _courseService.GetCourseCollectionByUser(token, userId);
        }

        [HttpGet]
        [Route("GetInvitesByUser")]
        public IEnumerable<Course> GetInvitesByUser([FromUri] Token token)
        {
            return _courseService.GetInviteCollectionByUser(token, token.UserId);
        }

        [HttpPost]
        [Route("Update")]
        public void Update([FromUri] Token token, [FromBody] Course course)
        {
            _courseService.UpdateCourse(token, course);
        }

        [HttpDelete]
        [Route("Delete/{courseId}")]
        public void Delete(Guid courseId, [FromUri] Token token)
        {
            _courseService.DeleteCourse(token, courseId);
        }

        [HttpDelete]
        [Route("Delete-member/{courseId}/{userId}")]
        public void DeleteMember(Guid courseId, Guid userId, [FromUri] Token token)
        {
            _courseService.DeleteMember(token, courseId, userId);
        }
    }
}