using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IReviewService
    {
        void Create(Review review, Guid executorId);
        Review Get(Guid reviewId, Guid executorId);
        Review GetReviewBySolutionAndUser(Guid solutionId, Guid userId, Guid executorId);
        ICollection<Review> GetReviewsBySolution(Guid solutionId, Guid executorId);
        void Delete(Guid reviewId, Guid executorId);
    }
}