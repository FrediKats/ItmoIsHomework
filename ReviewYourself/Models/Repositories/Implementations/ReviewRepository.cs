using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using DbExtensions;
using ReviewYourself.Models.Tools;

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

                SQL.INSERT_INTO("Review (ReviewID, AuthorID, SolutionID, Posted)")
                    .VALUES(Guid.NewGuid(), review.AuthorId, review.SolutionId, DateTime.Now)
                    .ToCommand(connection)
                    .ExecuteNonQuery();

                //TODO: add ReviewCriteria
            }
        }

        public Review Read(Guid reviewId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("ReviewID = {0}", reviewId)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();
                var review = ReaderConvertor.ToReview(reader);
                return review;
            }
        }

        public ICollection<Review> ReadBySolution(Guid solutionId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("SolutionID = {0}", solutionId)
                    .ToCommand(connection)
                    .ExecuteReader();

                var reviewCollection = new List<Review>();
                while (reader.Read())
                {
                    reviewCollection.Add(ReaderConvertor.ToReview(reader));
                }

                return reviewCollection;
            }
        }

        public Review ReadReviewBySolutionAndUser(Guid solutionId, Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var reader = SQL
                    .SELECT("*")
                    .FROM("Review")
                    .WHERE("SolutionID = {0}", solutionId)
                    .WHERE("AuthorID = {0}", userId)
                    .ToCommand(connection)
                    .ExecuteReader();

                reader.Read();
                var review = ReaderConvertor.ToReview(reader);
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
            throw new NotImplementedException();
        }
    }
}