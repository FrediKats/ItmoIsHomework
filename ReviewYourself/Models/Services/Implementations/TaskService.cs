using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private ITaskRepository _taskRepository;
        private ITokenRepository _tokenRepository;
        public TaskService(ITaskRepository taskRepository, ITokenRepository tokenRepository)
        {
            _taskRepository = taskRepository;
            _tokenRepository = tokenRepository;
        }
        public void CreateTask(ResourceTask task, Token token)
        {
            throw new NotImplementedException();
        }

        public ResourceTask GetTask(Guid taskId, Token token)
        {
            throw new NotImplementedException();
        }

        public ICollection<ResourceTask> GetTaskByCourse(Guid courseId, Token token)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(Guid taskId, Token token)
        {
            throw new NotImplementedException();
        }
    }
}