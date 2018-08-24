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
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);
            var solution = InstanceFactory.Solution(token, courseTask.Id);
            solution = _solutionService.Create(solution, token);

            var review = InstanceFactory.Review(token, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token);

            Assert.IsNotNull(createdReview);
        }

        [TestMethod]
        public void GetReview()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);
            var solution = InstanceFactory.Solution(token, courseTask.Id);
            solution = _solutionService.Create(solution, token);

            var review = InstanceFactory.Review(token, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token);
            var reviewById = _reviewService.Get(createdReview.Id, token);

            Assert.IsNotNull(reviewById);
            Assert.AreEqual(token, reviewById.AuthorId);
            Assert.AreEqual(solution.Id, reviewById.SolutionId);
        }

        [TestMethod]
        public void GetSolutionReviews()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);
            var solution = InstanceFactory.Solution(token, courseTask.Id);
            solution = _solutionService.Create(solution, token);

            var review = InstanceFactory.Review(token, solution.Id, courseTask);
            var createdReview = _reviewService.Create(review, token);
            var reviews = _reviewService.GetReviewsBySolution(solution.Id, token);

            Assert.IsNotNull(reviews);
            Assert.AreEqual(1, reviews.Count(r => r.AuthorId == token));
        }

        [TestMethod]
        public void DeleteReviewTest()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();
            course = _courseService.Create(course, token);
            var courseTask = InstanceFactory.CourseTask(token, course.Id);
            courseTask = _courseTaskService.Create(courseTask, token);
            var solution = InstanceFactory.Solution(token, courseTask.Id);
            solution = _solutionService.Create(solution, token);

            var review = InstanceFactory.Review(token, solution.Id, courseTask);
            review = _reviewService.Create(review, token);
            _reviewService.Delete(review.Id, token);
            var deletedReview = _reviewService.Get(review.Id, token);

            Assert.IsNull(deletedReview);
        }
    }
}