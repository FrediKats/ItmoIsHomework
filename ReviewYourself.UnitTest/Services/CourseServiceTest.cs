using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class CourseServiceTest
    {
        private ICourseService _courseService;

        [TestInitialize]
        public void Init()
        {
            _courseService = ServiceFactory.CourseService();
        }

        [TestMethod]
        public void CreateTest()
        {
            var userId = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, userId);
            Assert.AreNotEqual(createdCourse.Id, Guid.Empty);
        }

        [TestMethod]
        public void GetById()
        {
            var userId = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, userId);
            var courseById = _courseService.Get(createdCourse.Id);
            Assert.IsNotNull(courseById);
        }

        [TestMethod]
        public void GetByName()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, token);
            var courseByName = _courseService.FindCourses(createdCourse.Title);

            Assert.AreEqual(1, courseByName.Count(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void UpdateCourse()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            throw new NotImplementedException();
        }

        [TestMethod]
        public void DeleteCourse()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, token);
            _courseService.Delete(createdCourse.Id, token);
            var deletedCourse = _courseService.Get(createdCourse.Id);

            Assert.IsNull(deletedCourse);
        }

        [TestMethod]
        public void DeleteCourse_WithoutPermission()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, token);
            _courseService.Delete(createdCourse.Id, otherUser);
            var deletedCourse = _courseService.Get(createdCourse.Id);

            Assert.IsNotNull(deletedCourse);
        }
    }
}