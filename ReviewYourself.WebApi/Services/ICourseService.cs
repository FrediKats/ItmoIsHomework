using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ICourseService
    {
        Course Create(Course course, Guid executorId);
        Course Get(Guid courseId);
        ICollection<Course> FindCourses(string courseName);
        Course Update(Course course, Guid executorId);
        void Delete(Guid courseId, Guid executorId);
    }
}