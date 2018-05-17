using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly ITokenRepository _tokenRepository;

        public ReviewService(IReviewRepository reviewRepository, ITokenRepository tokenRepository)
        {
            _reviewRepository = reviewRepository;
            _tokenRepository = tokenRepository;
        }

        public void CreateReview(Token token, Review review)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            _reviewRepository.Create(review);
        }

        public Review GetReview(Token token, Guid reviewId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _reviewRepository.Read(reviewId);
        }

        public Review GetReviewBySolutionAndUser(Token token, Guid solutionId, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _reviewRepository.ReadReviewBySolutionAndUser(solutionId, userId);
        }

        public ICollection<Review> GetReviewListBySolution(Token token, Guid solutionId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _reviewRepository.ReadBySolution(solutionId);
        }

        public void DeleteReview(Token token, Guid reviewId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            _reviewRepository.Delete(reviewId);
        }
    }
}