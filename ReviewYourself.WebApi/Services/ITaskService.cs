using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ITaskService
    {
        void CreateTask(CourseTask courseTask, Guid executorId);
        CourseTask GetTask(Guid taskId, Guid executorId);
        ICollection<CourseTask> GetTaskInCourse(Guid courseId, Guid executorId);
        void DeleteTask(Guid taskId, Guid executorId);
    }
}