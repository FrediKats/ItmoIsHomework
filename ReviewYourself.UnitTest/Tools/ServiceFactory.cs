using System;
using Microsoft.EntityFrameworkCore;
using ReviewYourself.WebApi.Services;
using ReviewYourself.WebApi.Services.Implementations;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.UnitTest.Tools
{
    public static class ServiceFactory
    {
        private static PeerReviewContext Options()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(Config.ConnectionString);
            var context = new PeerReviewContext(builder.Options);
            return context;
        }

        public static IAuthorizationService AuthorizationService()
        {
            return new AuthorizationService(Options(), new JwtTokenFactory());
        }

        public static ICourseService CourseService()
        {
            throw new NotImplementedException();
        }

        public static ICourseTaskService CourseTaskService()
        {
            throw new NotImplementedException();
        }

        public static IMemberService MemberService()
        {
            throw new NotImplementedException();
        }

        public static IReviewService ReviewService()
        {
            throw new NotImplementedException();
        }

        public static ISolutionService SolutionService()
        {
            throw new NotImplementedException();
        }

        public static IUserService UserService()
        {
            throw new NotImplementedException();
        }
    }
}