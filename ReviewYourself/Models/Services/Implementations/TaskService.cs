﻿using System;
using System.Collections.Generic;
using ReviewYourself.Models.Repositories;

namespace ReviewYourself.Models.Services.Implementations
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ITokenRepository _tokenRepository;

        public TaskService(ITaskRepository taskRepository, ITokenRepository tokenRepository)
        {
            _taskRepository = taskRepository;
            _tokenRepository = tokenRepository;
        }

        public void CreateTask(Token token, ResourceTask task)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }
            //TODO: request admins right
            _taskRepository.Create(task);
        }

        public ResourceTask GetTask(Token token, Guid taskId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _taskRepository.Read(taskId);
        }

        public ICollection<ResourceTask> GetTaskListByCourse(Token token, Guid courseId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }

            return _taskRepository.ReadByCourse(courseId);
        }

        public void DeleteTask(Token token, Guid taskId)
        {
            if (_tokenRepository.ValidateToken(token) == false)
            {
                throw new Exception("Wrong token info");
            }
            //TODO: request admins right

            _taskRepository.Delete(taskId);
        }
    }
}