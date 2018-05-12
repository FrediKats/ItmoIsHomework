using System;

namespace ReviewYourself.Models.Services
{
    public interface ITaskService
    {
        void CreateTask(ResourceTask task, Token token);
        void GetTask(Guid taskId, Token token);
        void GetTaskByCourse(Guid courseId, Token token);
        void DeleteTask(Guid taskId, Token token);
    }
}