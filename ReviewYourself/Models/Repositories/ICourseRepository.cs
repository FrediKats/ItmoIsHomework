using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories
{
    public interface ICourseRepository
    {
        void Create(Course course);
        void CreateMember(Guid courseId, Guid userId);
        Course Read(Guid courseId);
        ICollection<Course> ReadByUser(Guid userId);
        void Update(Course course);
        void Delete(Guid courseId);
        void DeleteMember(Guid courseId, Guid userId);
    }
}