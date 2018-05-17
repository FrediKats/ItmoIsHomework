using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories
{
    public interface IReviewRepository
    {
        void Create(Review review);
        Review Read(Guid reviewId);
        ICollection<Review> ReadBySolution(Guid solutionId);
        Review ReadReviewBySolutionAndUser(Guid solutionId, Guid userId);
        void Update(Review review);
        void Delete(Guid reviewId);
    }
}