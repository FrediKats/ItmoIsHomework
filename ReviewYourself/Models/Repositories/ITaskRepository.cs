using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories
{
    public interface ITaskRepository
    {
        void Create(ResourceTask task);
        ResourceTask Read(Guid id);
        ICollection<ResourceTask> ReadByCourse(Guid courseId);
        void Delete(Guid taskId);
    }
}