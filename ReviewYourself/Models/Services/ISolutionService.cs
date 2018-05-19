using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ISolutionService
    {
        void CreateSolution(Token token, Solution solution);
        Solution GetSolution(Token token, Guid solutionId);
        Solution GetSolutionByTaskAndUser(Token token, Guid solutionId, Guid userId);
        ICollection<Solution> GetSolutionListByTask(Token token, Guid taskId);
        void DeleteSolution(Token token, Guid solutionId);

        bool IsCanAddReview(Token token, Guid solutionId);
        void ResolveSolution(Token token, Guid solutionId, Review review);
    }
}