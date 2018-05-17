using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private ITokenRepository _tokenRepository;

        public TaskService(ITaskRepository taskRepository, ITokenRepository tokenRepository)
        {
            _taskRepository = taskRepository;
            _tokenRepository = tokenRepository;
        }

        public void CreateTask(Token token, ResourceTask task)
        {
            _taskRepository.Create(task);
        }

        public ResourceTask GetTask(Token token, Guid taskId)
        {
            return _taskRepository.Read(taskId);
        }

        public ICollection<ResourceTask> GetTaskByCourse(Token token, Guid courseId)
        {
            return _taskRepository.ReadByCourse(courseId);
        }

        public void DeleteTask(Token token, Guid taskId)
        {
            _taskRepository.Delete(taskId);
        }
    }
}