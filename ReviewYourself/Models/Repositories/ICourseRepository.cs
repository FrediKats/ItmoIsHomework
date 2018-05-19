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
        ICollection<Course> ReadInvitesByUser(Guid userId);
        ICollection<ResourceUser> ReadMembersByCourse(Guid courseId);
        ICollection<ResourceUser> ReadInvitedByCourse(Guid courseId);
        void Update(Course course);
        void Delete(Guid courseId);
        void DeleteMember(Guid courseId, Guid userId);

        void AcceptInvite(Guid courseId, Guid userId);
        bool IsMember(Guid courseId, Guid userId);
    }
}