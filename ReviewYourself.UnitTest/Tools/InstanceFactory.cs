using System;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.UnitTest.Tools
{
    public static class InstanceFactory
    {
        public static Course Course()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public static AuthorizeData AuthorizeData(RegistrationData data)
        {
            throw new NotImplementedException();
        }

        public static Guid AuthorizedUserId()
        {
            var authorizationService = ServiceFactory.AuthorizationService();

            var regData = RegistrationData();
            var userId = authorizationService.RegisterMember(regData).UserId;
            return userId;
        }
    }
}