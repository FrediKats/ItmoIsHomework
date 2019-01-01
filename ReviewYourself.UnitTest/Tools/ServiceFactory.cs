using Microsoft.EntityFrameworkCore;
using ReviewYourself.WebApi.Services;
using ReviewYourself.WebApi.Services.Implementations;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.UnitTest.Tools
{
    public static class ServiceFactory
    {
        static ServiceFactory()
        {
            var context = CreateContext();
            AuthorizationService = new PeerReviewAuthService(context, new JwtTokenFactory());
            MemberService = new MemberService(context);
            CourseService = new CourseService(context, MemberService);
            CourseTaskService = new CourseServiceTask(context, MemberService);
            SolutionService = new SolutionService(context, MemberService);
            UserService = new PeerReviewUserService(context);
        }

        public static IPeerReviewAuthService AuthorizationService { get; }
        public static ICourseService CourseService { get; }
        public static ICourseTaskService CourseTaskService { get; }
        public static IMemberService MemberService { get; }
        public static IReviewService ReviewService { get; }
        public static ISolutionService SolutionService { get; }
        public static IPeerReviewUserService UserService { get; }

        private static PeerReviewContext CreateContext()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlServer(Config.ConnectionString);
            var context = new PeerReviewContext(builder.Options);
            return context;
        }
    }
}