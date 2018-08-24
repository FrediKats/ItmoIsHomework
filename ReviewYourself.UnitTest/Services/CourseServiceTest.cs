using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class CourseServiceTest
    {
        private IAuthorizationService _authorizationService;
        private ICourseService _courseService;

        [TestInitialize]
        public void Init()
        {
            _authorizationService = ServiceFactory.AuthorizationService();
            _courseService = ServiceFactory.CourseService();
        }

        [TestMethod]
        public void CreateTest()
        {
            var user = InstanceFactory.User();
        }
    }
}