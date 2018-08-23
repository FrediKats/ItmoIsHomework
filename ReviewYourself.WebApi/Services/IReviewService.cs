using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IReviewService
    {
        void CreateReview(Review review, Guid executorId);
        Review GetReview(Guid reviewId, Guid executorId);
        Review GetReviewBySolutionAndUser(Guid reviewId, Guid userId, Guid executorId);
        ICollection<Review> GetReviewsBySolution(Guid reviewId, Guid executorId);
        void DeleteReview(Guid reviewId, Guid executorId);
    }
}