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
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);

            var solution = InstanceFactory.Solution(token, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token);

            Assert.IsNotNull(createdSolution);
            Assert.AreEqual(token, createdSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, createdSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetSolution()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);

            var solution = InstanceFactory.Solution(token, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token);
            var newSolution = _solutionService.Get(createdSolution.Id, token);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(token, newSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, newSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetUserSolution()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);

            var solution = InstanceFactory.Solution(token, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token);
            var newSolution = _solutionService.GetUserSolution(courseTask.Id, token, token);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(token, newSolution.AuthorId);
            Assert.AreEqual(courseTask.Id, newSolution.CourseTaskId);
        }

        [TestMethod]
        public void GetTaskSolution()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);

            var solution = InstanceFactory.Solution(token, courseTask.Id);
            var createdSolution = _solutionService.Create(solution, token);
            var newSolution = _solutionService.GetSolutionsByTask(courseTask.Id, token);

            Assert.IsNotNull(newSolution);
            Assert.AreEqual(1, newSolution.Count(s => s.AuthorId == token));
        }

        [TestMethod]
        public void DeleteSolution()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);

            var solution = InstanceFactory.Solution(token, courseTask.Id);
            solution = _solutionService.Create(solution, token);
            _solutionService.Delete(solution.Id, token);
            var deletedSolution = _solutionService.Get(solution.Id, token);

            Assert.IsNull(deletedSolution);
        }
    }
}