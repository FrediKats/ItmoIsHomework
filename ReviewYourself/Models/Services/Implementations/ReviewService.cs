using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private IReviewRepository _reviewRepository;
        private ITokenRepository _tokenRepository;

        public ReviewService(IReviewRepository reviewRepository, ITokenRepository tokenRepository)
        {
            _reviewRepository = reviewRepository;
            _tokenRepository = tokenRepository;
        }

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