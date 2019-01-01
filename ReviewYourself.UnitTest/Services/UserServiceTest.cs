using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.Exceptions;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class UserServiceTest
    {
        private static readonly IPeerReviewUserService UserService;

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
        public void GetUser_NotFound()
        {
            var user = UserService.Get(Guid.NewGuid());

            Assert.IsNull(user);
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
        public void GetByName_NotFound()
        {
            var user = UserService.Get(InstanceFactory.GenerateString());

            Assert.IsNull(user);
        }

        [TestMethod]
        public void Update_Ok()
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
        public void Update_NoPermission()
        {
            var token = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();

            var user = UserService.Get(token);
            var newName = InstanceFactory.GenerateString();
            user.FirstName = newName;
            Assert.ThrowsException<PermissionDeniedException>(() => UserService.Update(user, otherUser));
        }

        [TestMethod]
        public void Disable()
        {
            throw new NotImplementedException();
        }
    }
}