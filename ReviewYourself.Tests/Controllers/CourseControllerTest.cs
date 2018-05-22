using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
    public class CourseControllerTest
    {
        private CourseController _courseController;
        private UserController _userController;

        [TestInitialize]
        public void Initialize()
        {
            _courseController = new CourseController(ServiceGenerator.GenerateCourseService());
            _userController = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void CreatorIsMentorTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData).Cast<Token>();

            var course = TemplateAction.CreateCourse(token, _courseController);
            var courseFullInfo = _courseController.GetById(course.Id, token).Cast<Course>();

            Assert.IsNotNull(courseFullInfo);
            Assert.AreEqual(courseFullInfo.Mentor.Id, token.UserId);
        }

        [TestMethod]
        public void InviteTest()
        {
            var mentorReg = InstanceGenerator.GenerateUser();
            var mentorAuth = InstanceGenerator.GenerateAuth(mentorReg);
            var studentReg = InstanceGenerator.GenerateUser();
            var studentAuth = InstanceGenerator.GenerateAuth(studentReg);

            _userController.SignUp(mentorReg);
            _userController.SignUp(studentReg);

            var mentorToken = _userController.SignIn(mentorAuth).Cast<Token>();
            var studentToken = _userController.SignIn(studentAuth).Cast<Token>();

            var course = TemplateAction.CreateCourse(mentorToken, _courseController);

            _courseController.InviteUser(course.Id, studentAuth.Login, mentorToken);
            var returnedCourse = _courseController
                .GetInvitesByUser(studentToken)
                .Cast<ICollection<Course>>()
                .First(c => c.Title == course.Title);

            Assert.IsNotNull(returnedCourse);
        }

        [TestMethod]
        public void AcceptInviteTest()
        {
            var mentorReg = InstanceGenerator.GenerateUser();
            var mentorAuth = InstanceGenerator.GenerateAuth(mentorReg);
            var studentReg = InstanceGenerator.GenerateUser();
            var studentAuth = InstanceGenerator.GenerateAuth(studentReg);

            _userController.SignUp(mentorReg);
            _userController.SignUp(studentReg);

            var mentorToken = _userController.SignIn(mentorAuth).Cast<Token>();
            var studentToken = _userController.SignIn(studentAuth).Cast<Token>();

            var course = TemplateAction.CreateCourse(mentorToken, _courseController);

            _courseController.InviteUser(course.Id, studentAuth.Login, mentorToken);
            _courseController.AcceptInvite(course.Id, studentToken);

            var isMember = _courseController
                .IsMember(course.Id, studentToken)
                .Cast<bool>();

            Assert.IsTrue(isMember);
        }
    }
}