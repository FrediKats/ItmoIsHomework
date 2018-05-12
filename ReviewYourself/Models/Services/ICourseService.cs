using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ICourseService
    {
        void AddCourse(Course course, Token token);
        void InviteUser(string username, Guid courseId, Token token);
        void AcceptInvite(Guid courseId, Token token);
        Course GetCourse(Guid courseId, Token token);
        ICollection<Course> GetCourseCollectionByUser(Guid userId, Token token);
        void UpdateCourse(Course course, Token token);
        void DeleteCourse(Guid courseId, Token token);
        void DeleteMember(Guid courseId, Guid userId, Token token);
    }
}