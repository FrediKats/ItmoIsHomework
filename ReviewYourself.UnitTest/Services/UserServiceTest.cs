using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private IAuthorizationService _authorizationService;
        private IUserService _userService;

        [TestInitialize]
        public void Init()
        {
            _authorizationService = ServiceFactory.AuthorizationService();
            _userService = ServiceFactory.UserService();
        }

        [TestMethod]
        public void GetUser()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var user = _userService.Get(token);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetByName()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var userById = _userService.Get(token);
            var userByName = _userService.Get(userById.Login);

            Assert.AreEqual(token, userByName.Id);
        }

        [TestMethod]
        public void Update()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Disable()
        {
            throw new NotImplementedException();
        }
    }
}