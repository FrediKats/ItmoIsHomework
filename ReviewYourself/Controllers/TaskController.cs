using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;

namespace ReviewYourself.Controllers
{
    public class TaskController : ApiController
    {
        [HttpPost]
        [Route("Add/{token}")]
        public void Add([FromUri]Token token, [FromBody]ResourceTask task)
        {
            throw new NotImplementedException();
        }
        
        [HttpGet]
        [Route("Get/{token}/{taskId}")]
        public ResourceTask Get([FromUri]Token token, [FromUri]Guid taskId)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("GetByCourse/{token}/{courseId}")]
        public IEnumerable<ResourceTask> GetByCourse([FromUri]Token token, [FromUri]Guid courseId)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route("Delete/{token}/{taskId}")]
        public void DeleteCourse([FromUri]Guid token, [FromUri]Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}
