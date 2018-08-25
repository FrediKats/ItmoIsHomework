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
        private static readonly IAuthorizationService AuthorizationService;

        static AuthorizationServiceTest()
        {
            AuthorizationService = ServiceFactory.AuthorizationService;
        }

        [TestMethod]
        public void RegistrationTest()
        {
            var regData = InstanceFactory.RegistrationData();
            AuthorizationService.RegisterMember(regData);
        }

        [TestMethod]
        public void LogIn_Correct()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);

            AuthorizationService.RegisterMember(regData);
            var token = AuthorizationService.LogIn(auth);

            Assert.IsNotNull(token);
            Assert.AreNotEqual(token.UserId, Guid.Empty);
        }

        [TestMethod]
        public void LogIn_IncorrectPassword()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);
            auth.Password = "new_wrong_password";

            AuthorizationService.RegisterMember(regData);

            Assert.ThrowsException<AuthenticationException>(() => AuthorizationService.LogIn(auth));
        }

        [TestMethod]
        public void UsernameAvailable_False()
        {
            var regData = InstanceFactory.RegistrationData();
            AuthorizationService.RegisterMember(regData);
            Assert.IsFalse(AuthorizationService.IsUsernameAvailable(regData.Login));
        }
    }
}