using System;
using System.Web.Http;
using System.Web.Http.Description;
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
        [ResponseType(typeof(void))]
        [Route("Add")]
        public IHttpActionResult Add([FromBody] ResourceTask task, [FromUri] Token token)
        {
            try
            {
                _taskService.CreateTask(token, task);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResourceTask))]
        [Route("GetById/{taskId}")]
        public IHttpActionResult Get(Guid taskId, [FromUri] Token token)
        {
            try
            {
                var result = _taskService.GetTask(token, taskId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [ResponseType(typeof(ResourceTask[]))]
        [Route("GetByCourse/{courseId}")]
        public IHttpActionResult GetByCourse(Guid courseId, [FromUri] Token token)
        {
            try
            {
                var result = _taskService.GetTaskListByCourse(token, courseId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [ResponseType(typeof(void))]
        [Route("Delete/{taskId}")]
        public IHttpActionResult DeleteCourse(Guid taskId, [FromUri] Token token)
        {
            try
            {
                _taskService.DeleteTask(token, taskId);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}