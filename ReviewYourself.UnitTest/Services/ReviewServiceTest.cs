using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class ReviewServiceTest
    {
        private ICourseService _courseService;
        private ICourseTaskService _courseTaskService;
        private IReviewService _reviewService;
        private ISolutionService _solutionService;

        [TestInitialize]
        public void Init()
        {
            _courseService = ServiceFactory.CourseService();
            _courseTaskService = ServiceFactory.CourseTaskService();
            _solutionService = ServiceFactory.SolutionService();
            _reviewService = ServiceFactory.ReviewService();
        }

        [TestMethod]
        public void CreateReview()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);
            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            solution = _solutionService.Create(solution, token.UserId);

            var review = InstanceFactory.Review(token.UserId, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token.UserId);

            Assert.IsNotNull(createdReview);
        }

        [TestMethod]
        public void GetReview()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);
            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            solution = _solutionService.Create(solution, token.UserId);

            var review = InstanceFactory.Review(token.UserId, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token.UserId);
            var reviewById = _reviewService.Get(createdReview.Id, token.UserId);

            Assert.IsNotNull(reviewById);
            Assert.AreEqual(token.UserId, reviewById.AuthorId);
            Assert.AreEqual(solution.Id, reviewById.SolutionId);
        }

        [TestMethod]
        public void GetSolutionReviews()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);
            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            solution = _solutionService.Create(solution, token.UserId);

            var review = InstanceFactory.Review(token.UserId, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token.UserId);
            var reviews = _reviewService.GetReviewsBySolution(solution.Id, token.UserId);

            Assert.IsNotNull(reviews);
            Assert.AreEqual(1, reviews.Count(r => r.AuthorId == token.UserId));
        }

        [TestMethod]
        public void DeleteReviewTest()
        {
            var token = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token.UserId);
            var courseTask = InstanceFactory.CourseTask(token.UserId, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token.UserId);
            var solution = InstanceFactory.Solution(token.UserId, courseTask.Id);
            solution = _solutionService.Create(solution, token.UserId);

            var review = InstanceFactory.Review(token.UserId, solution.Id, courseTask);
            review = _reviewService.Create(review, token.UserId);
            _reviewService.Delete(review.Id, token.UserId);
            var deletedReview = _reviewService.Get(review.Id, token.UserId);

            Assert.IsNull(deletedReview);
        }
    }
}