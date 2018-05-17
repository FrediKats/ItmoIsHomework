using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ReviewYourself.Models;
using ReviewYourself.Models.Services;

namespace ReviewYourself.Controllers
{
    [RoutePrefix("api/tasks")]
    public class TaskController : ApiController
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [Route("Add")]
        public void Add([FromBody]ResourceTask task, [FromUri]Token token)
        {
            _taskService.CreateTask(token, task);
        }
        
        [HttpGet]
        [Route("GetById/{taskId}")]
        public ResourceTask Get(Guid taskId, [FromUri]Token token)
        {
            return _taskService.GetTask(token, taskId);
        }

        [HttpGet]
        [Route("GetByCourse/{courseId}")]
        public IEnumerable<ResourceTask> GetByCourse(Guid courseId, [FromUri]Token token)
        {
            return _taskService.GetTaskByCourse(token, courseId);
        }

        [HttpDelete]
        [Route("Delete/{taskId}")]
        public void DeleteCourse(Guid taskId, [FromUri]Token token)
        {
            _taskService.DeleteTask(token, taskId);
        }
    }
}
