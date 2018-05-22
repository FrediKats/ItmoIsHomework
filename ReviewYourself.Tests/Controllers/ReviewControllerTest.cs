using System;
using System.Collections.Generic;
using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;
using ReviewYourself.Tests.Tools;

namespace ReviewYourself.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        private ReviewController _reviewController;
        private SolutionController _solutionController;
        private TaskController _taskController;
        private CourseController _courseController;
        private UserController _userController;

        [TestInitialize]
        public void Initialize()
        {
            _reviewController = new ReviewController(ServiceGenerator.GenerateReviewService());
            _solutionController = new SolutionController(ServiceGenerator.GenerateSolutionService());
            _taskController = new TaskController(ServiceGenerator.GenerateTaskService());
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void CreateReviewTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData).Cast<Token>();

            var course = TemplateAction.CreateCourse(token, _courseController);
            var task = TemplateAction.CreateTaskWithCriteria(token, course, _taskController);
            var solution = TemplateAction.CreateSolution(token, task, _solutionController);

            var review = new Review()
            {
                AuthorId = token.UserId,
                SolutionId = solution.Id,
                RateCollection = new List<ReviewCriteria>()
            };

            foreach (var criteria in task.CriteriaCollection)
            {
                review.RateCollection.Add(new ReviewCriteria()
                {
                    CriteriaId = criteria.Id,
                    Description = InstanceGenerator.GenerateString(),
                    Rating = 3
                });
            }

            _reviewController.Add(token, review);

            Assert.IsNotNull(solution);
            Assert.AreEqual(solution.TaskId, task.Id);
            Assert.AreEqual(solution.AuthorId, token.UserId);
        }
    }
}
