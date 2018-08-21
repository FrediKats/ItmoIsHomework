using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools.DataRecordExtensions;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private string _connectionString;

        public ReviewRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureConnect"].ConnectionString;
        }

        public static ReviewRepository Create(string connectionString)
        {
            return new ReviewRepository()
            {
                _connectionString = connectionString
            };
        }

        public void Create(Review review)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                review.Id = Guid.NewGuid();
                review.PostTime = DateTime.UtcNow;

                SQL.INSERT_INTO("Review (ReviewID, AuthorID, SolutionID, Posted)")
                    .VALUES(review.Id, review.AuthorId, review.SolutionId, review.PostTime)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                var insertRating = new SqlBuilder();

                foreach (var criteria in review.RateCollection)
                {
                    insertRating = insertRating
                        .INSERT_INTO("ReviewCriteria (ReviewID, CriteriaID, Rating, CriteriaDescription)")
                        .VALUES(review.Id, criteria.CriteriaId, criteria.Rating , criteria.Description)
                        .Append(";");
                }

                insertRating
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public Review Read(Guid reviewId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("ReviewID = {0}", reviewId)
                    .ToCommand(connection);

                Review review;

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    review = reader.GetReview();
                }

                review.RateCollection = new List<ReviewCriteria>();


                command = SQL
                    .SELECT("*")
                    .FROM("ReviewCriteria")
                    .WHERE("ReviewID = {0}", reviewId)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        review.RateCollection.Add(reader.GetReviewCriteria());
                    }
                }

                return review;
            }
        }

        public ICollection<Review> ReadBySolution(Guid solutionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection);

                ICollection<Review> reviewCollection = new List<Review>();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviewCollection.Add(reader.GetReview());
                    }
                }

                return reviewCollection;
            }
        }

        public Review ReadReviewBySolutionAndUser(Guid solutionId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var command = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("SolutionID = {0}", solutionId)
                    ._("AuthorID = {0}", userId)
                    .ToCommand(connection);

                Review review;

                using (var reader = command.ExecuteReader())
                {
                    reader.Read();
                    review = reader.GetReview();
                }

                command = SQL
                    .SELECT("*")
                    .FROM("ReviewCriteria")
                    .WHERE("ReviewID = {0}", review.Id)
                    .ToCommand(connection);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        review.RateCollection.Add(reader.GetReviewCriteria());
                    }
                }

                return review;
            }
        }

        public void Update(Review review)
        {
            //TODO: Remove?
            throw new NotImplementedException();
        }

        public void Delete(Guid reviewId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                SQL.DELETE_FROM("ReviewCriteria")
                    .WHERE("ReviewID = {0}", reviewId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                SQL.DELETE_FROM("Review")
                    .WHERE("ReviewID = {0}", reviewId)
                    .ToCommand(connection)
                    .ExecuteNonQuery();
            }
        }

        public bool CanPostReview(Guid solutionId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var permission = SQL
                    .SELECT("Permission")
                    .FROM("CourseMembership")
                    .JOIN("({0}) t ON CourseMembership.CourseID = t.CourseID",
                        SQL.SELECT("CourseID")
                            .FROM("ResourceTask")
                            .JOIN("({0}) s ON ResourceTask.TaskID = s.TaskID",
                                SQL.SELECT("TaskID")
                                    .FROM("Solution")
                                    .WHERE("SolutionID = {0}", solutionId)))
                    .WHERE("UserID = {0}", userId)
                    .ToCommand(connection)
                    .ExecuteScalar();

                return (int.Parse(permission?.ToString() ?? "0") > 0);
            }
        }
    }
}