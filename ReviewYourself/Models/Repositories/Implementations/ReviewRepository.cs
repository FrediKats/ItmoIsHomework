using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        public void Create(Review review)
        {
            throw new NotImplementedException();
        }

        public Review Read(Guid reviewId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> ReadBySolution(Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public Review ReadReviewBySolutionAndUser(Guid solutionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Review review)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid reviewId)
        {
            throw new NotImplementedException();
        }
    }
}