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
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);

            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token.UserId);

            Assert.IsNotNull(createdTask);
            Assert.AreEqual(token.UserId, createdTask.AuthorId);
            Assert.AreEqual(course.Id, createdTask.CourseId);
        }

        [TestMethod]
        public void GetTaskById()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);

            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token.UserId);
            var taskById = _courseTaskService.Get(createdTask.Id, token.UserId);

            Assert.IsNotNull(taskById);
            Assert.AreEqual(token.UserId, taskById.AuthorId);
            Assert.AreEqual(course.Id, taskById.CourseId);
        }

        [TestMethod]
        public void GetTaskInCourse()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);

            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token.UserId);
            var taskInList = _courseTaskService.GetTaskInCourse(course.Id, token.UserId);

            Assert.AreEqual(1, taskInList.Count(t => t.Id == createdTask.Id));
        }

        [TestMethod]
        public void DeleteTask()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);

            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            var createdTask = _courseTaskService.Create(courseTask, token.UserId);
            _courseTaskService.Delete(createdTask.Id, token.UserId);
            var taskInList = _courseTaskService.Get(createdTask.Id, token.UserId);

            Assert.IsNull(taskInList);
        }
    }
}