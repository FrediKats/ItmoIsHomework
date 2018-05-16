using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;
using ReviewYourself.Models.Tools;
using ReviewYourself.Tests.Tools;

namespace ReviewYourself.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private UserController _controller;

        [ClassInitialize]
        public void Initialize()
        {
            _controller = new UserController(new UserService(new UserRepository(), new TokenRepository()));
        }

        [TestMethod]
        public void SignUpTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData);

            Assert.IsNotNull(token);
            Assert.IsNotNull(token.TokenUserId);
        }

        [TestMethod]
        public void ReadingUserTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };

            _controller.SignUp(regData);
            var userByUsername = _controller.FindByUsername(regData.Login, null);
            var userById = _controller.GetUser(userByUsername.Id, null);

            Assert.AreEqual(userByUsername.FirstName, userById.FirstName);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };

            _controller.SignUp(regData);
            var user = _controller.FindByUsername(regData.Login, null);
            user.Biography = "New bio";
            _controller.UpdateUser(null, user);

            var newUser = _controller.FindByUsername(regData.Login, null);

            Assert.AreEqual(user.Biography, newUser.Biography);
        }
    }
}
