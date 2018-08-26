using System;
using System.Collections.Generic;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Services
{
    public interface IMemberService
    {
        void SendInvite(Guid courseId, Guid targetId, Guid executorId);
        void AcceptInvite(Guid courseId, Guid executorId);
        void DenyInvite(Guid courseId, Guid executorId);

        ICollection<Course> GetUserCourses(Guid userId);
        ICollection<Course> GetUserInvitations(Guid userId);
        ICollection<User> GetMembers(Guid courseId);
        ICollection<User> GetMentors(Guid courseId);
        bool IsMentor(Guid courseId, Guid memberId);
        bool IsMember(Guid courseId, Guid memberId);

        void MakeMentor(Guid courseId, Guid targetId, Guid executorId);
        void DeleteMember(Guid courseId, Guid targetId, Guid executorId);
    }
}