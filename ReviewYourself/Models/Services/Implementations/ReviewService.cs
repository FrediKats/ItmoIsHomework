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

        public void CreateReview(Review review, Token token)
        {
            var user = _tokenRepository.GetUserByToken(token);
            if (user == null)
            {
                throw new Exception();
            }

            if (review.AuthorId != user.Id)
            {
                throw new Exception();
            }

            _reviewRepository.Create(review);
        }

        public Review GetReview(Guid reviewId, Token token)
        {
            return _reviewRepository.Read(reviewId);
        }

        public Review GetReviewBySolutionAndUser(Guid reviewId, Guid userId, Token token)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewBySolution(Guid solutionId, Token token)
        {
            return _reviewRepository.ReadBySolution(solutionId);
        }

        public void DeleteReview(Guid reviewId, Token token)
        {
            _reviewRepository.Delete(reviewId);
        }
    }
}