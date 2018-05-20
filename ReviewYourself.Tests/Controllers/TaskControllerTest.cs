using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models;
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

        [TestInitialize]
        public void Initialize()
        {
            _taskController = new TaskController(ServiceGenerator.GenerateTaskService());
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void TaskCreationTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);
            var course = TemplateAction.CreateCourse(token, _courseController);
            var task = TemplateAction.CreateTask(token, course, _taskController);

            Assert.IsNotNull(task);
            Assert.AreEqual(task.CourseId, course.Id);
            Assert.AreEqual(task.Description, task.Description);
        }

        [TestMethod]
        public void TaskWithCriteriaCreateTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);
            var course = TemplateAction.CreateCourse(token, _courseController);


            var task = TemplateAction.CreateTaskWithCriteria(token, course, _taskController);

            Assert.IsNotNull(task);
            Assert.AreEqual(task.CourseId, course.Id);
            Assert.AreEqual(task.Description, task.Description);
        }
    }
}
