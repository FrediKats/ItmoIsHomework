using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly ITokenRepository _tokenRepository;
        private IReviewRepository _reviewRepository;

        public SolutionService(ISolutionRepository solutionRepository, IReviewRepository reviewRepository,
            ITokenRepository tokenRepository)
        {
            _solutionRepository = solutionRepository;
            _reviewRepository = reviewRepository;
            _tokenRepository = tokenRepository;
        }

        public void CreateSolution(Token token, Solution solution)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            //TODO: check if user in course
            _solutionRepository.Create(solution);
        }

        public Solution GetSolution(Token token, Guid solutionId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _solutionRepository.Read(solutionId);
        }

        public Solution GetSolutionByTaskAndUser(Token token, Guid taskId, Guid userId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _solutionRepository.ReadByTaskAndUser(taskId, userId);
        }

        public ICollection<Solution> GetSolutionListByTask(Token token, Guid taskId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _solutionRepository.ReadByTask(taskId);
        }

        public void DeleteSolution(Token token, Guid solutionId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            //TODO: mentors rights
            _solutionRepository.Delete(solutionId);
        }

        public bool IsCanAddReview(Token token, Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Token token, Guid solutionId, Review review)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            _solutionRepository.ResolveSolution(solutionId);
        }
    }
}