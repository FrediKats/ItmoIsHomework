using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface IReviewService
    {
        void CreateReview(Token token, Review review);
        Review GetReview(Token token, Guid reviewId);
        Review GetReviewBySolutionAndUser(Token token, Guid solutionId, Guid userId);
        ICollection<Review> GetReviewListBySolution(Token token, Guid solutionId);
        void DeleteReview(Token token, Guid reviewId);
    }
}