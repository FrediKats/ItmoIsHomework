using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Repositories.Implementations
{
    public class CourseRepository : ICourseRepository
    {
        public void Create(Course course)
        {
            throw new NotImplementedException();
        }

        public void CreateMember(Guid courseId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public Course Read(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> ReadByUser(Guid userId)
        {
            throw new NotImplementedException();
        }

        public void Update(Course course)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid courseId)
        {
            throw new NotImplementedException();
        }

        public void DeleteMember(Guid courseId, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}