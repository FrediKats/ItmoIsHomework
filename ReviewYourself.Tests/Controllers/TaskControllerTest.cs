using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;
using ReviewYourself.Models.Tools;
using ReviewYourself.Tests.Tools;

namespace ReviewYourself.Tests.Controllers
{
    [TestClass]
    public class TaskControllerTest
    {
        private TaskController _taskController;
        private CourseController _courseController;
        private UserController _userController;

        [ClassInitialize]
        public void Initialize()
        {
            _taskController = new TaskController(ServiceGenerator.GenerateTaskService());
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void TaskCreation()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);
            var course = InstanceGenerator.GenerateCourse();
            var task = InstanceGenerator.GenerateTask();

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);

            _courseController.Create(token, course);
            var currentCourse = _courseController.GetByUser(token).First(c => c.Title == course.Title);
            task.CourseId = currentCourse.Id;
            _taskController.Add(task, token);

            var resultTask = _taskController
                .GetByCourse(currentCourse.Id, token)
                .First(t => t.Title == task.Title && t.Description == task.Description);

            Assert.IsNotNull(resultTask);
            Assert.AreEqual(resultTask.CourseId, course.Id);
            Assert.AreEqual(resultTask.Description, task.Description);
        }
    }
}
