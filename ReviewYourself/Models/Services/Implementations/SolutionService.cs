using System;

namespace ReviewYourself.Models.Services.Implementations
{
    public class SolutionService : ISolutionService
    {
        public void CreateSolution(Solution solution, Token token)
        {
            throw new NotImplementedException();
        }

        public void GetSolution(Guid solutionId, Token token)
        {
            throw new NotImplementedException();
        }

        public void GetSolutionByTask(Guid taskId, Token token)
        {
            throw new NotImplementedException();
        }

        public void DeleteSolution(Guid solutionId, Token token)
        {
            throw new NotImplementedException();
        }

        public bool IsCanAddReview(Guid solutionId, Token token)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Guid solutionId, Review review, Token token)
        {
            throw new NotImplementedException();
        }
    }
}