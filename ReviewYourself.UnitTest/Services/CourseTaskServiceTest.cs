using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class CourseTaskServiceTest
    {
        private ICourseService _courseService;
        private ICourseTaskService _courseTaskService;

        [TestInitialize]
        public void Init()
        {
            _courseService = ServiceFactory.CourseService();
            _courseTaskService = ServiceFactory.CourseTaskService();
        }

        [TestMethod]
        public void CreateTask()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);

            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token);

            Assert.IsNotNull(createdTask);
            Assert.AreEqual(token, createdTask.AuthorId);
            Assert.AreEqual(course.Id, createdTask.CourseId);
        }

        [TestMethod]
        public void GetTaskById()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);

            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token);
            var taskById = _courseTaskService.Get(createdTask.Id, token);

            Assert.IsNotNull(taskById);
            Assert.AreEqual(token, taskById.AuthorId);
            Assert.AreEqual(course.Id, taskById.CourseId);
        }

        [TestMethod]
        public void GetTaskInCourse()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);

            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token);
            var taskInList = _courseTaskService.GetTaskInCourse(course.Id, token);

            Assert.AreEqual(1, taskInList.Count(t => t.Id == createdTask.Id));
        }

        [TestMethod]
        public void DeleteTask()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);

            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token);
            _courseTaskService.Delete(createdTask.Id, token);
            var taskInList = _courseTaskService.Get(createdTask.Id, token);

            Assert.IsNull(taskInList);
        }
    }
}