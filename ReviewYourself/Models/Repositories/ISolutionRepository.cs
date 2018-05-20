using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories
{
    public interface ISolutionRepository
    {
        void Create(Solution solution);
        Solution Read(Guid solutionId);
        ICollection<Solution> ReadByTask(Guid taskId);
        Solution ReadByTaskAndUser(Guid taskId, Guid userId);
        void Delete(Guid solutionId);
        void ResolveSolution(Guid solutionId);
        bool IsCanPostSolution(Guid taskId, Guid userId);
    }
}