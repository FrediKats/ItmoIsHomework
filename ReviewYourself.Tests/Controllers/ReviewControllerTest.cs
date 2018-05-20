using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
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

        [ClassInitialize]
        public void Initialize()
        {
            _reviewController = new ReviewController(ServiceGenerator.GenerateReviewService());
            _solutionController = new SolutionController(ServiceGenerator.GenerateSolutionService());
            _taskController = new TaskController(ServiceGenerator.GenerateTaskService());
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
