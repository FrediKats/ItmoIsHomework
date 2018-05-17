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

        public Review GetReview(Token token, Guid reviewId)
        {
            return _reviewRepository.Read(reviewId);
        }

        public Review GetReviewBySolutionAndUser(Token token, Guid solutionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Review> GetReviewBySolution(Token token, Guid solutionId)
        {
            return _reviewRepository.ReadBySolution(solutionId);
        }

        public void DeleteReview(Token token, Guid reviewId)
        {
            _reviewRepository.Delete(reviewId);
        }
    }
}