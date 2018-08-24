using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ICourseTaskService
    {
        CourseTask Create(CourseTask courseTask, Guid executorId);
        CourseTask Get(Guid taskId, Guid executorId);
        ICollection<CourseTask> GetTaskInCourse(Guid courseId, Guid executorId);
        void Delete(Guid taskId, Guid executorId);
    }
}