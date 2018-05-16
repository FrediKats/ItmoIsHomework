using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ISolutionService
    {
        void CreateSolution(Solution solution, Token token);
        Solution GetSolution(Guid solutionId, Token token);
        Solution GetSolutionByTaskAndUser(Guid solutionId, Guid userId, Token token);
        ICollection<Solution> GetSolutionByTask(Guid taskId, Token token);
        void DeleteSolution(Guid solutionId, Token token);

        bool IsCanAddReview(Guid solutionId, Token token);
        void ResolveSolution(Guid solutionId, Review review, Token token);
    }
}