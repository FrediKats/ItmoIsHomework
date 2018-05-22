using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Results;
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
    public class SolutionControllerTest
    {
        private SolutionController _solutionController;
        private TaskController _taskController;
        private CourseController _courseController;
        private UserController _userController;

        [TestInitialize]
        public void Initialize()
        {
            _solutionController = new SolutionController(ServiceGenerator.GenerateSolutionService());
            _taskController = new TaskController(ServiceGenerator.GenerateTaskService());
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void SolutionCreateTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData).Cast<Token>();

            var course = TemplateAction.CreateCourse(token, _courseController);
            var task = TemplateAction.CreateTask(token, course, _taskController);
            var solution = TemplateAction.CreateSolution(token, task, _solutionController);

            Assert.IsNotNull(solution);
            Assert.AreEqual(solution.TaskId, task.Id);
            Assert.AreEqual(solution.AuthorId, token.UserId);
        }

        [TestMethod]
        public void SolutionGetByUser()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData).Cast<Token>();

            var course = TemplateAction.CreateCourse(token, _courseController);
            var task = TemplateAction.CreateTask(token, course, _taskController);
            var solution = TemplateAction.CreateSolution(token, task, _solutionController);

            var result = _solutionController
                .GetByTaskAndUser(task.Id, token.UserId, token)
                .Cast<Solution>();

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TaskId, task.Id);
            Assert.AreEqual(result.AuthorId, token.UserId);
        }

        [TestMethod]
        public void SolutionGetByTask()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData).Cast<Token>();

            var course = TemplateAction.CreateCourse(token, _courseController);
            var task = TemplateAction.CreateTask(token, course, _taskController);
            var solution = TemplateAction.CreateSolution(token, task, _solutionController);

            var result = _solutionController.GetByTask(task.Id, token)
                .Cast<ICollection<Solution>>()
                .First(s => s.TextData == solution.TextData);

            Assert.IsNotNull(result);
            Assert.AreEqual(result.TaskId, task.Id);
            Assert.AreEqual(result.AuthorId, token.UserId);
        }
    }
}
