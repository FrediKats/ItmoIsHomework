using System;
using Microsoft.EntityFrameworkCore;
using ReviewYourself.WebApi.Services;
using ReviewYourself.WebApi.Services.Implementations;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.UnitTest.Tools
{
    public static class ServiceFactory
    {
        private static readonly IAuthorizationService _authorizationService;
        private static ICourseService _courseService;
        private static ICourseTaskService _courseTaskService;
        private static IMemberService _memberService;

        static ServiceFactory()
        {
            _authorizationService = new AuthorizationService(Options(), new JwtTokenFactory());
        }

        private static PeerReviewContext Options()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(Config.ConnectionString);
            var context = new PeerReviewContext(builder.Options);
            return context;
        }

        public static IAuthorizationService AuthorizationService()
        {
            return _authorizationService;
        }

        public static ICourseService CourseService()
        {
            return _courseService;
        }

        public static ICourseTaskService CourseTaskService()
        {
            return _courseTaskService;
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