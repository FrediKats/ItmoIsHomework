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

        public void CreateSolution(Solution solution, Token token)
        {
            _solutionRepository.Create(solution);
        }

        public Solution GetSolution(Guid solutionId, Token token)
        {
            return _solutionRepository.Read(solutionId);
        }

        public Solution GetSolutionByTaskAndUser(Guid solutionId, Guid userId, Token token)
        {
            throw new NotImplementedException();
        }

        public ICollection<Solution> GetSolutionByTask(Guid taskId, Token token)
        {
            return _solutionRepository.ReadByTask(taskId);
        }

        public void DeleteSolution(Guid solutionId, Token token)
        {
            _solutionRepository.Delete(solutionId);
        }

        public bool IsCanAddReview(Guid solutionId, Token token)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Guid solutionId, Review review, Token token)
        {
            _solutionRepository.ResolveSolution(solutionId);
        }
    }
}