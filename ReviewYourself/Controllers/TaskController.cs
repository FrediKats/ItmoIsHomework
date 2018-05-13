using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        [HttpPost]
        //[Route("Add")]
        public void Add([FromBody]ResourceTask task, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("Get/{taskId}")]
        public ResourceTask Get(Guid taskId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetByCourse/{courseId}")]
        public IEnumerable<ResourceTask> GetByCourse(Guid courseId, [FromUri]Token token)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("{taskId}")]
        public void DeleteCourse(Guid taskId, [FromUri]Guid token)
        {
            throw new NotImplementedException();
        }
    }
}
