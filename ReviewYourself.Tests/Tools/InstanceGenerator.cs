using System;
using System.Linq;
using ReviewYourself.Models;
using ReviewYourself.Models.Tools;

namespace ReviewYourself.Tests.Tools
{
    public static class InstanceGenerator
    {
        private static readonly Random Random = new Random();

        public static string GenerateString(int size = 15)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            var res = Enumerable.Range(1, size).Select(s => chars[Random.Next(chars.Length)]).ToArray();
            return new string(res);
        }

        public static RegistrationData GenerateRegistration()
        {
            return new RegistrationData()
            {
                FirstName = GenerateString(),
                LastName = GenerateString(),
                Login = GenerateString(),
                Password = GenerateString()
            };
        }

        public static Course GenerateCourse()
        {
            return new Course()
            {
                Title = GenerateString(),
                Description = GenerateString(),
            };
        }

        public static ResourceTask GenerateTask()
        {
            return new ResourceTask()
            {
                Description = GenerateString(),
                PostTime = DateTime.Today,
                Title = GenerateString()
            };
        }

        public static Solution GenerateSolution()
        {
            return new Solution()
            {
                TextData = GenerateString(),
                PostTime = DateTime.Today,
                IsResolved = false,

            };
        }

        public static AuthorizeData GenerateAuth(RegistrationData regData)
        {
            return new AuthorizeData()
            {
                Login = regData.Login,
                Password = regData.Password
            };
        }
    }
}