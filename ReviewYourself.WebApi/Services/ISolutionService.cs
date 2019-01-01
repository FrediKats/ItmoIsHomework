using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ISolutionService
    {
        CourseSolution Create(CourseSolution courseSolution, Guid executorId);
        CourseSolution Get(Guid solutionId, Guid executorId);
        CourseSolution GetUserSolution(Guid taskId, Guid userId, Guid executorId);
        ICollection<CourseSolution> GetSolutionsByTask(Guid taskId, Guid executor);
        void Delete(Guid solutionId, Guid executorId);

        //void ResolveSolution(Guid solutionId, Guid executorId);
    }
}