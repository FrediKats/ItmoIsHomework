﻿using System;
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
            _taskController = new TaskController(new TaskService(new TaskRepository(), new TokenRepository()));
            _courseController = new CourseController(new CourseService(new CourseRepository(),
                new UserRepository(), new TokenRepository()));
            _userController = new UserController(new UserService(new UserRepository(), new TokenRepository()));
        }

        [TestMethod]
        public void TaskCreation()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };
            _userController.SignUp(regData);
            var course = InstanceGenerator.GenerateCourse();
            var task = InstanceGenerator.GenerateTask();
            var token = _userController.SignIn(authData);

            _courseController.Create(token, course);
            var currentCourse = _courseController.GetByUser(token).First(c => c.Title == course.Title);
            task.CourseId = currentCourse.Id;
            _taskController.Add(task, token);

            var resultTask = _taskController.GetByCourse(currentCourse.Id, token).First(t => t.Title == task.Title);

            Assert.AreEqual(resultTask.CourseId, course.Id);
            Assert.AreEqual(resultTask.Description, task.Description);
        }
    }
}
