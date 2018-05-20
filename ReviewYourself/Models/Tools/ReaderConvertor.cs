using System;
using System.Collections.Generic;
using System.Data.Common;
using DbExtensions;

namespace ReviewYourself.Models.Tools
{
    public static class ReaderConvertor
    {
        public static ResourceUser ToUser(DbDataReader reader, string prefix = "")
        {
            return new ResourceUser
            {
                Id = Guid.Parse(reader[$"{prefix}UserID"].ToString()),
                Login = reader.GetString($"{prefix}UserLogin"),
                Email = reader.GetStringOrNull($"{prefix}Email"),
                FirstName = reader.GetString($"{prefix}FirstName"),
                LastName = reader.GetString($"{prefix}LastName"),
                Biography = reader.GetStringOrNull($"{prefix}Bio")
            };
        }

        public static Course ToCourse(DbDataReader reader, string prefix = "")
        {
            //TODO: think about mentor
            return new Course
            {
                Id = Guid.Parse(reader[$"{prefix}CourseID"].ToString()),
                Title = reader.GetString($"{prefix}Title"),
                Description = reader.GetStringOrNull($"{prefix}CourseDescription"),
                //Mentor = new ResourceUser
                //{
                //    Id = Guid.Parse(reader["UserID"].ToString()),
                //    Login = reader["UserLogin"].ToString(),
                //    Email = reader["Email"].ToString(),
                //    FirstName = reader["FirstName"].ToString(),
                //    LastName = reader["LastName"].ToString(),
                //    Biography = reader["Bio"].ToString()
                //}
            };
        }

        public static ResourceTask ToTask(DbDataReader reader, string prefix = "")
        {
            return new ResourceTask
            {
                Id = Guid.Parse(reader[$"{prefix}TaskID"].ToString()),
                CourseId = Guid.Parse(reader[$"{prefix}CourseID"].ToString()),
                //CriteriaCollection = new List<Criteria>(),
                Title = reader.GetString($"{prefix}Title"),
                Description = reader.GetString($"{prefix}TaskDescription"),
                PostTime = reader.GetDateTime($"{prefix}Posted")
            };
        }

        public static Solution ToSolution(DbDataReader reader, string prefix = "")
        {
            return new Solution
            {
                Id = Guid.Parse(reader[$"{prefix}SolutionID"].ToString()),
                AuthorId = Guid.Parse(reader[$"{prefix}AuthorID"].ToString()),
                TaskId = Guid.Parse(reader[$"{prefix}TaskID"].ToString()),
                TextData = reader.GetString($"{prefix}TextData"),
                PostTime = DateTime.Parse(reader[$"{prefix}Posted"].ToString()),
                IsResolved = reader.GetBoolean($"{prefix}Resolved")
                //AttachmentCollection = new List<SqlFileStream>()
            };
        }

        public static Review ToReview(DbDataReader reader, string prefix = "")
        {
            return new Review()
            {
                Id = Guid.Parse(reader[$"{prefix}SolutionID"].ToString()),
                AuthorId = Guid.Parse(reader[$"{prefix}AuthorID"].ToString()),
                SolutionId = Guid.Parse(reader[$"{prefix}SolutionID"].ToString()),
                PostTime = reader.GetDateTime($"{prefix}Posted"),
            };
        }
    }
}