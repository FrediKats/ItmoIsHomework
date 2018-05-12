using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class SolutionRepository : ISolutionRepository
    {
        public void Create(Solution solution)
        {
            throw new NotImplementedException();
        }

        public Solution Read(Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Solution> ReadByTask(Guid taskId)
        {
            throw new NotImplementedException();
        }

        public Solution ReadByTaskAndUser(Guid taskId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid solutionId)
        {
            throw new NotImplementedException();
        }

        public void ResolveSolution(Guid solutionId)
        {
            throw new NotImplementedException();
        }
    }
}