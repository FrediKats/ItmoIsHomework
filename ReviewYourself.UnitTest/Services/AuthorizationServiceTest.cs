using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class AuthorizationServiceTest
    {
        private IAuthorizationService _authorizationService;

        [TestInitialize]
        public void Init()
        {
            _authorizationService = ServiceFactory.AuthorizationService();
        }

        [TestMethod]
        public void RegistrationTest()
        {
            var regData = InstanceFactory.RegistrationData();
            _authorizationService.RegisterMember(regData);
        }

        [TestMethod]
        public void LogIn()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);

            _authorizationService.RegisterMember(regData);
            var token = _authorizationService.LogIn(auth);

            Assert.IsNotNull(token);
            Assert.AreNotEqual(token.UserId, Guid.Empty);
        }

        [TestMethod]
        public void UsernameAvaliable_False()
        {
            var regData = InstanceFactory.RegistrationData();
            _authorizationService.RegisterMember(regData);
            Assert.IsFalse(_authorizationService.IsUsernameAvailable(regData.Login));
        }
    }
}