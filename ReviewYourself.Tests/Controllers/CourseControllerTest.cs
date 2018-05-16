using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;
using ReviewYourself.Models.Tools;
using ReviewYourself.Tests.Tools;

namespace ReviewYourself.Tests.Controllers
{
    [TestClass]
    public class CourseControllerTest
    {
        private CourseController _courseController;
        private UserController _userController;

        [ClassInitialize]
        public void Initialize()
        {
            _courseController = new CourseController(new CourseService(new CourseRepository(),
                new UserRepository(), new TokenRepository()));
            _userController = new UserController(new UserService(new UserRepository(), new TokenRepository()));
        }

        [TestMethod]
        public void IsCreatorMentorTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };
            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);
            var course = InstanceGenerator.GenerateCourse();
            var user = _userController.GetUser(token);

            //TODO: dont use course.Mentor on create
            _courseController.Create(token, course);
            var userCourseCollection = _courseController.GetByUser(token);
            var currentCourse = userCourseCollection.FirstOrDefault(c => c.Title == course.Title);

            Assert.IsNotNull(currentCourse);
            Assert.AreEqual(currentCourse.Mentor.Id, user.Id);
        }

        [TestMethod]
        public void InviteTest()
        {
            var mentorReg = InstanceGenerator.GenerateRegistration();
            var mentorAuth = new AuthorizeData()
            {
                Login = mentorReg.Login,
                Password = mentorReg.Password
            };
            _userController.SignUp(mentorReg);

            var studentReg = InstanceGenerator.GenerateRegistration();
            var studentAuth = new AuthorizeData()
            {
                Login = mentorReg.Login,
                Password = mentorReg.Password
            };
            _userController.SignUp(studentReg);

            var mentorToken = _userController.SignIn(mentorAuth);
            var course = InstanceGenerator.GenerateCourse();
            var student = _userController.FindByUsername(studentAuth.Login, mentorToken);

            _courseController.Create(mentorToken, course);
            var userCourseCollection = _courseController.GetByUser(mentorToken);
            //TODO: Title is unique
            var currentCourse = userCourseCollection.FirstOrDefault(c => c.Title == course.Title);
            _courseController.InviteUser(currentCourse.Id, studentAuth.Login, mentorToken);
            
            //TODO: Invite list in controller
            var studentInvite = _courseController.GetByUser(student.Id, mentorToken);
            var returnedCourse = studentInvite.FirstOrDefault(c => c.Title == currentCourse.Title);

            Assert.IsNotNull(returnedCourse);
        }
    }
}
