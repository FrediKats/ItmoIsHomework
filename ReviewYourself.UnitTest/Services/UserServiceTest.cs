using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private static readonly IUserService UserService;

        static UserServiceTest()
        {
            UserService = ServiceFactory.UserService;
        }

        [TestMethod]
        public void GetUser()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var user = UserService.Get(token);

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetByName()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var userById = UserService.Get(token);
            var userByName = UserService.Get(userById.Login);

            Assert.AreEqual(token, userByName.Id);
        }

        [TestMethod]
        public void Update()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var user = UserService.Get(token);
            var newName = InstanceFactory.GenerateString();
            user.FirstName = newName;
            UserService.Update(user, token);
            var updatedUser = UserService.Get(token);
            Assert.AreEqual(newName, updatedUser.FirstName);
        }

        [TestMethod]
        public void Disable()
        {
            throw new NotImplementedException();
        }
    }
}