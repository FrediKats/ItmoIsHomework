using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Exceptions;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class CourseServiceTest
    {
        private static readonly ICourseService CourseService;

        static CourseServiceTest()
        {
            CourseService = ServiceFactory.CourseService;
        }

        [TestMethod]
        public void CreateTest()
        {
            var userId = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, userId);
            Assert.AreNotEqual(createdCourse.Id, Guid.Empty);
        }

        [TestMethod]
        public void GetById()
        {
            var userId = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, userId);
            var courseById = CourseService.Get(createdCourse.Id);
            Assert.IsNotNull(courseById);
        }

        [TestMethod]
        public void GetById_NotFound()
        {
            var course = CourseService.Get(Guid.Empty);

            Assert.IsNull(course);
        }

        [TestMethod]
        public void GetByName()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, token);
            var courseByName = CourseService.FindCourses(createdCourse.Title);

            Assert.AreEqual(1, courseByName.Count(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void GetByName_NotFound()
        {
            var course = CourseService.FindCourses("some_wrong_name");

            Assert.AreEqual(0, course.Count);
        }

        [TestMethod]
        public void UpdateCourse()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, token);
            var newTitle = InstanceFactory.GenerateString();
            createdCourse.Title = newTitle;
            CourseService.Update(createdCourse, token);
            var updatedCourse = CourseService.Update(course, token);

            Assert.AreEqual(newTitle, updatedCourse.Title);
        }

        [TestMethod]
        public void UpdateCourse_WithoutPermission()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, token);

            Assert.ThrowsException<PermissionDeniedException>(()
                => CourseService.Update(createdCourse, otherUser));
        }

        [TestMethod]
        public void DeleteCourse()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, token);
            CourseService.Delete(createdCourse.Id, token);
            var deletedCourse = CourseService.Get(createdCourse.Id);

            Assert.IsNull(deletedCourse);
        }

        [TestMethod]
        public void DeleteCourse_WithoutPermission()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = CourseService.Create(course, token);

            Assert.ThrowsException<PermissionDeniedException>(()
                => CourseService.Delete(createdCourse.Id, otherUser));
        }
    }
}