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
    public class CourseController : ApiController
    {
        // POST: Add/{token}
        [HttpPost]
        [Route("Add")]
        public void Add([FromUri]Token token, [FromBody]Course course)
        {
            throw new NotImplementedException();
        }

        // POST: Invite/{token}/{courseId}/{username}
        [HttpPost]
        [Route("Invite/{courseId}/{username}")]
        public void InviteUser(Guid courseId, string username, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        // POST: Accept-Invite/{token}/{courseId}
        [HttpPost]
        [Route("Accept-Invite/{courseId}")]
        public void AcceptInvite(Guid courseId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        // GET: Get/{token}/{courseId}
        [HttpGet]
        [Route("Get/{courseId}")]
        public Course Get(Guid courseId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        // GET: GetByUser/{token}/{userId}
        [HttpGet]
        [Route("GetByUser/{userId}")]
        public IEnumerable<Course> GetByUser(Guid userId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        // POST: Update/{token}
        [HttpPost]
        [Route("Update")]
        public void Update([FromUri]Token token, [FromBody]Course course)
        {
            throw new NotImplementedException();
        }

        // DELETE: Delete/{token}/{courseId}
        [HttpDelete]
        [Route("Delete/{courseId}")]
        public void Delete(Guid courseId, [FromUri]Token token)
        {
            
            throw new NotImplementedException();
        }

        // DELETE: Delete-member/{token}/{courseId}/{userId}
        [HttpDelete]
        [Route("Delete-member/{courseId}/{userId}")]
        public void DeleteMember(Guid courseId, Guid userId,[FromUri]Token token)
        {
            throw new NotImplementedException();
        }
    }
}
