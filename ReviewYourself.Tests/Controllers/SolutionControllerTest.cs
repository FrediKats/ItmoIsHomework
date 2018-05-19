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
    public class SolutionControllerTest
    {
        private SolutionController _solutionController;
        private TaskController _taskController;
        private CourseController _courseController;
        private UserController _userController;

        [ClassInitialize]
        public void Initialize()
        {
            _solutionController = new SolutionController(new SolutionService(new SolutionRepository(), new ReviewRepository(), new TokenRepository()));
            _taskController = new TaskController(new TaskService(new TaskRepository(), new TokenRepository()));
            _courseController = new CourseController(new CourseService(new CourseRepository(),
                new UserRepository(), new TokenRepository()));
            _userController = new UserController(new UserService(new UserRepository(), new TokenRepository()));
        }

        [TestMethod]
        public void SolutionCreateTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = InstanceGenerator.GenerateAuth(regData);
            var course = InstanceGenerator.GenerateCourse();
            var task = InstanceGenerator.GenerateTask();
            var solution = InstanceGenerator.GenerateSolution();


            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);
            _courseController.Create(token, course);

            var currentCourse = _courseController.GetByUser(token)
                .First(c => c.Title == course.Title);
            task.CourseId = currentCourse.Id;
            _taskController.Add(task, token);

            task = _taskController.GetByCourse(currentCourse.Id, token)
                .First(t => t.Title == task.Title);
            solution.TaskId = task.Id;
            _solutionController.Add(token, solution);

            var resultSolution = _solutionController.GetByTaskAndUser(task.Id, token.UserId, token);

            Assert.IsNotNull(resultSolution);
            Assert.AreEqual(solution.PostTime, resultSolution.PostTime);
            Assert.AreEqual(solution.IsResolved, resultSolution.IsResolved);
        }
    }
}
