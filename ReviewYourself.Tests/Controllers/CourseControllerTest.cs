using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
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
            var authData = InstanceGenerator.GenerateAuth(regData);
            var course = InstanceGenerator.GenerateCourse();

            _userController.SignUp(regData);
            var token = _userController.SignIn(authData);
            var user = _userController.GetUser(token);

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
            var mentorAuth = InstanceGenerator.GenerateAuth(mentorReg);
            var studentReg = InstanceGenerator.GenerateRegistration();
            var studentAuth = InstanceGenerator.GenerateAuth(studentReg);
            var course = InstanceGenerator.GenerateCourse();

            _userController.SignUp(mentorReg);
            _userController.SignUp(studentReg);

            var mentorToken = _userController.SignIn(mentorAuth);
            var studentToken = _userController.SignIn(studentAuth);

            _courseController.Create(mentorToken, course);
            var currentCourse = _courseController
                .GetByUser(mentorToken)
                .First(c => c.Title == course.Title);

            Assert.IsNotNull(currentCourse);

            _courseController.InviteUser(currentCourse.Id, studentAuth.Login, mentorToken);
            var returnedCourse = _courseController
                .GetInvitesByUser(studentToken)
                .First(c => c.Title == currentCourse.Title);

            Assert.IsNotNull(returnedCourse);
        }
    }
}