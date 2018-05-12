using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        public void CreateReview(Review review, Token token)
        {
            throw new NotImplementedException();
        }

        public Review GetReview(Guid reviewId, Token token)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewBySolution(Guid solutionId, Token token)
        {
            throw new NotImplementedException();
        }

        public void DeleteReview(Guid reviewId, Token token)
        {
            throw new NotImplementedException();
        }
    }
}