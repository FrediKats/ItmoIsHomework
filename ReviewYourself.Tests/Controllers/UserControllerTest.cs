using System.Web.Http.Results;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models;
using ReviewYourself.Tests.Tools;

namespace ReviewYourself.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;

        [TestInitialize]
        public void Initialize()
        {
            _controller = new UserController(ServiceGenerator.GenerateUserService());
        }

        [TestMethod]
        public void SignUpTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData).Cast<Token>();

            Assert.IsNotNull(token);
            Assert.IsNotNull(token.TokenData);
        }

        [TestMethod]
        public void ReadingUserTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);

            var token = _controller.SignIn(authData).Cast<Token>();
            var userByUsername = _controller.GetByUsername(regData.Login, token).Cast<ResourceUser>();
            var userById = _controller.GetById(userByUsername.Id, token).Cast<ResourceUser>();

            Assert.AreEqual(userByUsername.FirstName, userById.FirstName);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var regData = InstanceGenerator.GenerateUser();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData).Cast<Token>();
            var user = _controller.GetByUsername(regData.Login, token).Cast<ResourceUser>();

            user.Biography = "New bio";
            _controller.UpdateUser(token, user);
            var updatedUser = _controller.GetByUsername(regData.Login, token).Cast<ResourceUser>();

            Assert.AreEqual(user.Biography, updatedUser.Biography);
        }
    }
}