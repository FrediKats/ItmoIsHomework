using System;

namespace ReviewYourself.Models.Services
{
    public interface ISolutionService
    {
        void CreateSolution(Solution solution, Token token);
        void GetSolution(Guid solutionId, Token token);
        void GetSolutionByTask(Guid taskId, Token token);
        void DeleteSolution(Guid solutionId, Token token);

        bool IsCanAddReview(Guid solutionId, Token token);
        void ResolveSolution(Guid solutionId, Review review, Token token);
    }
}