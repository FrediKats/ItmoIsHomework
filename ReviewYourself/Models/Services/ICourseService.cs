using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ICourseService
    {
        void AddCourse(Course course, Token token);
        Course GetCourse(Guid courseId, Token token);
        ICollection<Course> GetCourseCollectionByUser(Token token);
        ICollection<Course> GetInviteCollectionByUser(Token token);
        ICollection<Course> GetCourseCollectionByUser(Guid userId, Token token);
        ICollection<Course> GetInviteCollectionByUser(Guid userId, Token token);
        void UpdateCourse(Course course, Token token);
        void DeleteCourse(Guid courseId, Token token);
        void DeleteMember(Guid courseId, Guid userId, Token token);

        void InviteUser(string username, Guid courseId, Token token);
        void AcceptInvite(Guid courseId, Token token);
    }
}