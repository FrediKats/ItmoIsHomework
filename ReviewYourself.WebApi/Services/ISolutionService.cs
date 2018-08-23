using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ISolutionService
    {
        void CreateSolution(Solution solution, Guid executor);
        Solution GetSolution(Guid solutionId, Guid executorId);
        Solution GetSolutionByTaskAndUser(Guid solutionId, Guid userId, Guid executorId);
        ICollection<Solution> GetSolutionsByTask(Guid taskId, Guid executor);
        void DeleteSolution(Guid solutionId, Guid executorId);

        //void ResolveSolution(Guid solutionId, Guid executorId);
    }
}