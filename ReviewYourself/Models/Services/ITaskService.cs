using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ITaskService
    {
        void CreateTask(ResourceTask task, Token token);
        ResourceTask GetTask(Guid taskId, Token token);
        ICollection<ResourceTask> GetTaskByCourse(Guid courseId, Token token);
        void DeleteTask(Guid taskId, Token token);
    }
}