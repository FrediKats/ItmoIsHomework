using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class SolutionServiceTest
    {
        private ICourseService _courseService;
        private ICourseTaskService _courseTaskService;
        private ISolutionService _solutionService;

        [TestInitialize]
        public void Init()
        {
            _courseService = ServiceFactory.CourseService();
            _courseTaskService = ServiceFactory.CourseTaskService();
            _solutionService = ServiceFactory.SolutionService();
        }

        [TestMethod]
        public void CreateSolution()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);

            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token.UserId);

            Assert.IsNotNull(createdSolution);
            Assert.AreEqual(token.UserId, createdSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, createdSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetSolution()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);

            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token.UserId);
            var newSolution = _solutionService.Get(createdSolution.Id, token.UserId);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(token.UserId, newSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, newSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetUserSolution()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);

            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token.UserId);
            var newSolution = _solutionService.GetUserSolution(courseTask.Id, token.UserId, token.UserId);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(token.UserId, newSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, newSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetTaskSolution()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);

            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token.UserId);
            var newSolution = _solutionService.GetSolutionsByTask(courseTask.Id, token.UserId);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(1, newSolution.Count(s => s.AuthorId == token.UserId));
        }

        [TestMethod]
        public void DeleteSolution()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);

            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            solution = _solutionService.Create(solution, token.UserId);
            _solutionService.Delete(solution.Id, token.UserId);
            var deletedSolution = _solutionService.Get(solution.Id, token.UserId);

            Assert.IsNull(deletedSolution);
        }
    }
}