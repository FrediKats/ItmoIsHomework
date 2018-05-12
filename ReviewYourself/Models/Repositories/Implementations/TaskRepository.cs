using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class TaskRepository : ITaskRepository
    {
        public void Create(ResourceTask task)
        {
            throw new NotImplementedException();
        }

        public ResourceTask Read(Guid id)
        {
            throw new NotImplementedException();
        }

        public ICollection<ResourceTask> ReadByCourse(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid taskId)
        {
            throw new NotImplementedException();
        }
    }
}