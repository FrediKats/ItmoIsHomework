using System;
using System.Collections.Generic;

namespace ReviewYourself.Models.Services
{
    public interface ICourseService
    {
        void AddCourse(Token token, Course course);
        Course GetCourse(Token token, Guid courseId);
        ICollection<Course> GetCourseCollectionByUser(Token token, Guid userId);
        ICollection<Course> GetInviteCollectionByUser(Token token, Guid userId);
        void UpdateCourse(Token token, Course course);
        void DeleteCourse(Token token, Guid courseId);
        void DeleteMember(Token token, Guid courseId, Guid userId);

        void InviteUser(Token token, string username, Guid courseId);
        void AcceptInvite(Token token, Guid courseId);
        bool IsMember(Token token, Guid courseId);
    }
}