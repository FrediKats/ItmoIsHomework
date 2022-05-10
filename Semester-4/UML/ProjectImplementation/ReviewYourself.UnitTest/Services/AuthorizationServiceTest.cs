using System;
using System.Data;
using System.Security.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class AuthorizationServiceTest
    {
        private static readonly IPeerReviewAuthService AuthorizationService;

        static AuthorizationServiceTest()
        {
            AuthorizationService = ServiceFactory.AuthorizationService;
        }

        [TestMethod]
        public void RegistrationTest_Ok()
        {
            var regData = InstanceFactory.RegistrationData();
            AuthorizationService.RegisterMember(regData);
        }

        [TestMethod]
        public void RegistrationTest_EmptyPassword()
        {
            var regData = InstanceFactory.RegistrationData();
            regData.Password = null;
            AuthorizationService.RegisterMember(regData);
        }

        [TestMethod]
        public void RegistrationTest_DuplicateException()
        {
            var regData = InstanceFactory.RegistrationData();

            AuthorizationService.RegisterMember(regData);

            Assert.ThrowsException<DuplicateNameException>(() => AuthorizationService.RegisterMember(regData));
        }

        [TestMethod]
        public void LogIn_Ok()
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
            RegistrationData regData = InstanceFactory.RegistrationData();
            AuthData auth = InstanceFactory.AuthorizeData(regData);
            auth.Password = "new_wrong_password";

            AuthorizationService.RegisterMember(regData);

            Assert.ThrowsException<AuthenticationException>(() => AuthorizationService.LogIn(auth));
        }

        [TestMethod]
        public void LogOut()
        {
            var regData = InstanceFactory.RegistrationData();
            var auth = InstanceFactory.AuthorizeData(regData);

            AuthorizationService.RegisterMember(regData);
            var token = AuthorizationService.LogIn(auth);
            AuthorizationService.LogOut(token);
        }

        [TestMethod]
        public void UsernameAvailable_Ok()
        {
            Assert.IsTrue(AuthorizationService.IsUsernameAvailable(InstanceFactory.GenerateString()));
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