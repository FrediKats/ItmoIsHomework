using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.Controllers;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;
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
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData);

            Assert.IsNotNull(token);
            Assert.IsNotNull(token.TokenData);
        }

        [TestMethod]
        public void ReadingUserTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData);

            var userByUsername = _controller.GetByUsername(regData.Login, token);
            var userById = _controller.GetById(userByUsername.Id, token);

            Assert.AreEqual(userByUsername.FirstName, userById.FirstName);
        }

        [TestMethod]
        public void UpdateUserTest()
        {
            var regData = InstanceGenerator.GenerateRegistration();
            var authData = InstanceGenerator.GenerateAuth(regData);

            _controller.SignUp(regData);
            var token = _controller.SignIn(authData);

            var user = _controller.GetByUsername(regData.Login, token);
            user.Biography = "New bio";
            _controller.UpdateUser(token, user);
            var updatedUser = _controller.GetByUsername(regData.Login, token);

            Assert.AreEqual(user.Biography, updatedUser.Biography);
        }
    }
}