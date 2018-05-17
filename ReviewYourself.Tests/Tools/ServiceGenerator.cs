using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReviewYourself.Models.Repositories.Implementations;
using ReviewYourself.Models.Services.Implementations;

namespace ReviewYourself.Tests.Tools
{
    public static class  ServiceGenerator
    {
        private static readonly string ConnectionString;

        static ServiceGenerator()
        {
            ConnectionString =
                ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }
        public static UserService GenerateUserService()
        {
            return new UserService(new UserRepository(ConnectionString),
                new TokenRepository(ConnectionString));
        }

        public static CourseService GenerateCourseService()
        {
            return new CourseService(new CourseRepository(ConnectionString),
                new UserRepository(ConnectionString),
                new TokenRepository(ConnectionString));
        }
    }
}
