using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private IReviewRepository _reviewRepository;
        private ITokenRepository _tokenRepository;

        public SolutionService(ISolutionRepository solutionRepository, IReviewRepository reviewRepository,
            ITokenRepository tokenRepository)
        {
            _solutionRepository = solutionRepository;
            _reviewRepository = reviewRepository;
            _tokenRepository = tokenRepository;
        }

        public void CreateSolution(Token token, Solution solution)
        {
            _solutionRepository.Create(solution);
        }

        public Solution GetSolution(Token token, Guid solutionId)
        {
            return _solutionRepository.Read(solutionId);
        }

        public Solution GetSolutionByTaskAndUser(Token token, Guid solutionId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Solution> GetSolutionByTask(Token token, Guid taskId)
        {
            return _solutionRepository.ReadByTask(taskId);
        }

        public void DeleteSolution(Token token, Guid solutionId)
        {
            _solutionRepository.Delete(solutionId);
        }

        public bool IsCanAddReview(Token token, Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Token token, Guid solutionId, Review review)
        {
            _solutionRepository.ResolveSolution(solutionId);
        }
    }
}