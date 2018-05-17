using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ITaskService
    {
        void CreateTask(Token token, ResourceTask task);
        ResourceTask GetTask(Token token, Guid taskId);
        ICollection<ResourceTask> GetTaskByCourse(Token token, Guid courseId);
        void DeleteTask(Token token, Guid taskId);
    }
}