using System;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class AuthorizationServiceTest
    {
        private static readonly IAuthorizationService _authorizationService;

        static AuthorizationServiceTest()
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
        public void LogIn_Correct()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);

            _authorizationService.RegisterMember(regData);
            var token = _authorizationService.LogIn(auth);

            Assert.IsNotNull(token);
            Assert.AreNotEqual(token.UserId, Guid.Empty);
        }

        [TestMethod]
        public void LogIn_IncorrectPassword()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);
            auth.Password = "new_wrong_password";

            _authorizationService.RegisterMember(regData);

            Assert.ThrowsException<AuthenticationException>(() => _authorizationService.LogIn(auth));
        }

        [TestMethod]
        public void UsernameAvailable_False()
        {
            var regData = InstanceFactory.RegistrationData();
            _authorizationService.RegisterMember(regData);
            Assert.IsFalse(_authorizationService.IsUsernameAvailable(regData.Login));
        }
    }
}