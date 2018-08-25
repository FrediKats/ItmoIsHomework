using System;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.UnitTest.Tools
{
    public static class InstanceFactory
    {
        private static readonly Random Random = new Random((int) DateTime.UtcNow.Ticks);

        public static Course Course()
        {
            return new Course()
            {
                Title = GenerateString(),
                Description = GenerateString()
            };
        }

        public static CourseTask CourseTask(Guid authorId, Guid courseId)
        {
            throw new NotImplementedException();
        }

        public static Criteria Criteria(Guid courseTaskId)
        {
            throw new NotImplementedException();
        }

        public static Review Review(Guid authorId, Guid solutionId, CourseTask courseTask)
        {
            throw new NotImplementedException();
        }

        public static Solution Solution(Guid authorId, Guid courseTaskId)
        {
            throw new NotImplementedException();
        }

        public static User User()
        {
            throw new NotImplementedException();
        }

        public static RegistrationData RegistrationData()
        {
            return new RegistrationData
            {
                Login = GenerateString(),
                FirstName = GenerateString(),
                LastName = GenerateString(),
                Password = GenerateString(),
                Email = GenerateString()
            };
        }

        public static AuthorizeData AuthorizeData(RegistrationData data)
        {
            return new AuthorizeData
            {
                Login = data.Login,
                Password = data.Password
            };
        }

        public static Guid AuthorizedUserId()
        {
            var authorizationService = ServiceFactory.AuthorizationService;

            var regData = RegistrationData();
            var userId = authorizationService.RegisterMember(regData).UserId;
            return userId;
        }

        public static string GenerateString(int size = 15)
        {
            const string chars = "qwertyuiopasdfghjklzxcvbnmABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var res = Enumerable.Range(1, size).Select(s => chars[Random.Next(chars.Length)]).ToArray();
            return new string(res);
        }
    }
}