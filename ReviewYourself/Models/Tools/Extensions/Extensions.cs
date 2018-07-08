using DbExtensions;
using System;
using System.Data;

namespace ReviewYourself.Models.Tools.Extensions
{
    public static class Extensions
    {
        public static Guid GetGuid(this IDataRecord record, string name)
        {
            return record.GetGuid(record.GetOrdinal(name));
        }

        public static Course GetCourse(this IDataRecord record)
        {
            return new Course
            {
                Id = record.GetGuid("CourseID"),
                Title = record.GetString("Title"),
                Description = record.GetStringOrNull("CourseDescription")
            };
        }

        public static ResourceUser GetResourceUser(this IDataRecord record)
        {
            return new ResourceUser
            {
                Id = record.GetGuid("UserID"),
                Login = record.GetString("UserLogin"),
                Email = record.GetStringOrNull("Email"),
                FirstName = record.GetString("FirstName"),
                LastName = record.GetString("LastName"),
                Biography = record.GetStringOrNull("Bio")
            };
        }

        public static ResourceTask GetResourceTask(this IDataRecord record)
        {
            return new ResourceTask
            {
                Id = record.GetGuid("TaskID"),
                CourseId = record.GetGuid("CourseID"),
                Title = record.GetString("Title"),
                Description = record.GetString("TaskDescription"),
                PostTime = record.GetDateTime("Posted")
            };
        }

        public static Solution GetSolution(this IDataRecord record)
        {
            return new Solution
            {
                Id = record.GetGuid("SolutionID"),
                AuthorId = record.GetGuid("AuthorID"),
                TaskId = record.GetGuid("TaskID"),
                TextData = record.GetString("TextData"),
                PostTime = record.GetDateTime("Posted"),
                IsResolved = record.GetBoolean("Resolved")
            };
        }

        public static Review GetReview(this IDataRecord record)
        {
            return new Review
            {
                Id = record.GetGuid("ReviewID"),
                AuthorId = record.GetGuid("AuthorID"),
                SolutionId = record.GetGuid("SolutionID"),
                PostTime = record.GetDateTime($"Posted"),
            };
        }

        public static Criteria GetCriteria(this IDataRecord record)
        {
            return new Criteria
            {
                Id = record.GetGuid("CriteriaID"),
                TaskId = record.GetGuid("TaskID"),
                Title = record.GetString("Title"),
                Description = record.GetStringOrNull("CriteriaDescription"),
                MaxPoint = record.GetInt32("MaxPoint")
            };
        }

        public static ReviewCriteria GetReviewCriteria(this IDataRecord record)
        {
            return new ReviewCriteria
            {
                ReviewId = record.GetGuid("ReviewID"),
                CriteriaId = record.GetGuid("CriteriaID"),
                Rating = record.GetInt32("Rating"),
                Description = record.GetStringOrNull("CriteriaDescription")
            };
        }
    }
}