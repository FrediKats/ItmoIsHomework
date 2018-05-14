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
        public void CreateTask(ResourceTask task, Token token)
        {
            _taskRepository.Create(task);
        }

        public ResourceTask GetTask(Guid taskId, Token token)
        {
            return _taskRepository.Read(taskId);
        }

        public ICollection<ResourceTask> GetTaskByCourse(Guid courseId, Token token)
        {
            return _taskRepository.ReadByCourse(courseId);
        }

        public void DeleteTask(Guid taskId, Token token)
        {
            _taskRepository.Delete(taskId);
        }
    }
}