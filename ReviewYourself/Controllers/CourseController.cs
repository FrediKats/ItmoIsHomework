using System;
using System.Web.Http;
using System.Web.Http.Description;
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
        [ResponseType(typeof(void))]
        public IHttpActionResult Create([FromUri] Token token, [FromBody] Course course)
        {
            try
            {
                _courseService.CreateCourse(token, course);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Invite/{courseId}/{username}")]
        public IHttpActionResult InviteUser(Guid courseId, string username, [FromUri] Token token)
        {
            try
            {
                _courseService.InviteUser(token, username, courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Accept-Invite/{courseId}")]
        public IHttpActionResult AcceptInvite(Guid courseId, [FromUri] Token token)
        {
            try
            {
                _courseService.AcceptInvite(token, courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(bool))]
        [Route("IsMember/{courseId}")]
        public IHttpActionResult IsMember(Guid courseId, [FromUri] Token token)
        {
            try
            {
                var result = _courseService.IsMember(token, courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [HttpGet]
        [ResponseType(typeof(Course))]
        [Route("GetById/{courseId}")]
        public IHttpActionResult GetById(Guid courseId, [FromUri] Token token)
        {
            try
            {
                var result = _courseService.GetCourse(token, courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Course[]))]
        [Route("GetByUser")]
        public IHttpActionResult GetByUser([FromUri] Token token)
        {
            try
            {
                var result = _courseService.GetCourseListByUser(token, token.UserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Course[]))]
        [Route("GetByUser/{userId}")]
        public IHttpActionResult GetByUser(Guid userId, [FromUri] Token token)
        {
            try
            {
                var result = _courseService.GetCourseListByUser(token, userId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(Course[]))]
        [Route("GetInvitesByUser")]
        public IHttpActionResult GetInvitesByUser([FromUri] Token token)
        {
            try
            {
                var result = _courseService.GetInviteListByUser(token, token.UserId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [ResponseType(typeof(void))]
        [Route("Update")]
        public IHttpActionResult Update([FromUri] Token token, [FromBody] Course course)
        {
            try
            {
                _courseService.UpdateCourse(token, course);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        [Route("Delete/{courseId}")]
        public IHttpActionResult Delete(Guid courseId, [FromUri] Token token)
        {
            try
            {
                _courseService.DeleteCourse(token, courseId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        [Route("Delete-member/{courseId}/{userId}")]
        public IHttpActionResult DeleteMember(Guid courseId, Guid userId, [FromUri] Token token)
        {
            try
            {
                _courseService.DeleteMember(token, courseId, userId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}