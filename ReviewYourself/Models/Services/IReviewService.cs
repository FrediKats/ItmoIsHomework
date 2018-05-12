using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface IReviewService
    {
        void CreateReview(Review review, Token token);
        Review GetReview(Guid reviewId, Token token);
        ICollection<Review> GetReviewBySolution(Guid solutionId, Token token);
        void DeleteReview(Guid reviewId, Token token);
    }
}