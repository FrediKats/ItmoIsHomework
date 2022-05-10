using System;
using System.Collections.Generic;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly PeerReviewContext _context;

        public ReviewService(PeerReviewContext context)
        {
            _context = context;
        }

        public Review Create(Review review, Guid executorId)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review;
        }

        public Review Get(Guid reviewId, Guid executorId)
        {
            return _context.Reviews.FirstOrDefault(review => review.Id == reviewId);
        }

        public Review GetReviewBySolutionAndUser(Guid solutionId, Guid userId, Guid executorId)
        {
            return _context.Reviews.FirstOrDefault(review => review.SolutionId == solutionId
                                                             && review.AuthorId == userId);
        }

        //TODO: Whe collection?
        public ICollection<Review> GetReviewsBySolution(Guid solutionId, Guid executorId)
        {
            return _context.Reviews.Where(review => review.SolutionId == solutionId).ToList();
        }

        public void Delete(Guid reviewId, Guid executorId)
        {
            Review review = _context.Reviews.First(r => r.Id == reviewId);
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }
    }
}