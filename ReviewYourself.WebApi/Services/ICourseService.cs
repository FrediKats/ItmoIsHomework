using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface ICourseService
    {
        Course CreateCourse(Course course, Guid executorId);

        Course GetCourse(Guid courseId);
        ICollection<Course> FindCourses(string courseName);
        void UpdateCourse(Course course, Guid executorId);
        void DeleteCourse(Guid courseId, Guid executorId);
    }
}