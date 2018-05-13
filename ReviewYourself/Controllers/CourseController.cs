using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/controllers")]
    public class CourseController : ApiController
    {
        [HttpPost]
        [Route("Add")]
        public void Add([FromUri]Token token, [FromBody]Course course)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Invite/{courseId}/{username}")]
        public void InviteUser(Guid courseId, string username, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Accept-Invite/{courseId}")]
        public void AcceptInvite(Guid courseId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("Get/{courseId}")]
        public Course Get(Guid courseId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetByUser/{userId}")]
        public IEnumerable<Course> GetByUser(Guid userId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("Update")]
        public void Update([FromUri]Token token, [FromBody]Course course)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete/{courseId}")]
        public void Delete(Guid courseId, [FromUri]Token token)
        {
            
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete-member/{courseId}/{userId}")]
        public void DeleteMember(Guid courseId, Guid userId,[FromUri]Token token)
        {
            throw new NotImplementedException();
        }
    }
}
