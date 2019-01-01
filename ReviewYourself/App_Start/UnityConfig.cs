 using System.Web.Http;
 using ReviewYourself.Models.Repositories;
 using ReviewYourself.Models.Repositories.Implementations;
 using Unity;
using Unity.WebApi;
using ReviewYourself.Models.Services;
using ReviewYourself.Models.Services.Implementations;

namespace ReviewYourself
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<ICourseRepository, CourseRepository>();
            container.RegisterType<IReviewRepository, ReviewRepository>();
            container.RegisterType<ISolutionRepository, SolutionRepository>();
            container.RegisterType<ITaskRepository, TaskRepository>();
            container.RegisterType<ITokenRepository, TokenRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<ICourseService, CourseService>();
            container.RegisterType<IReviewService, ReviewService>();
            container.RegisterType<ISolutionService, SolutionService>();
            container.RegisterType<ITaskService, TaskService>();
            container.RegisterType<IUserService, UserService>();

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}