using System;
using System.Collections.Generic;
using System.Linq;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Models;
using ReviewYourself.WebApi.Tools;

namespace ReviewYourself.WebApi.Services.Implementations
{
    public class MemberService : IMemberService
    {
        private readonly PeerReviewContext _context;

        public MemberService(PeerReviewContext context)
        {
            _context = context;
        }

        public void SendInvite(Guid courseId, Guid targetId, Guid executorId)
        {
            var participation = new Participation
            {
                CourseId = courseId,
                MemberId = targetId,
                Permission = MemberPermission.Invited
            };
            _context.Participations.Add(participation);
            _context.SaveChanges();
        }

        public void AcceptInvite(Guid courseId, Guid executorId)
        {
            var participation = _context.Participations
                .FirstOrDefault(p => p.MemberId == executorId && p.CourseId == courseId);

            if (participation == null)
            {
                throw new ArgumentException("Invite not found");
            }

            if (participation.Permission == MemberPermission.None)
            {
                throw new Exception("Database data exception");
            }

            if (participation.Permission == MemberPermission.Invited)
            {
                participation.Permission = MemberPermission.Member;
                _context.Participations.Update(participation);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Already in course");
            }
        }

        public void DenyInvite(Guid courseId, Guid executorId)
        {
            var participation = _context.Participations
                .FirstOrDefault(p => p.MemberId == executorId && p.CourseId == courseId);

            if (participation == null)
            {
                throw new ArgumentException("Invite not found");
            }

            if (participation.Permission == MemberPermission.None)
            {
                throw new Exception("Database data exception");
            }

            if (participation.Permission == MemberPermission.Invited)
            {
                _context.Participations.Remove(participation);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("Already in course");
            }
        }

        public ICollection<Course> GetUserCourses(Guid userId)
        {
            return _context.Participations
                .Where(p => p.MemberId == userId && p.Permission != MemberPermission.Invited)
                .Join(_context.Courses, p => p.CourseId, c => c.Id, (p, c) => c)
                .ToList();
        }

        public ICollection<Course> GetUserInvitations(Guid userId)
        {
            return _context.Participations
                .Where(p => p.MemberId == userId && p.Permission == MemberPermission.Invited)
                .Join(_context.Courses, participation => participation.CourseId, course => course.Id, (p, c) => c)
                .ToList();
        }

        public ICollection<PeerReviewUser> GetMembers(Guid courseId)
        {
            return _context.Participations
                .Where(p => p.CourseId == courseId &&
                            (p.Permission == MemberPermission.Member
                             || p.Permission == MemberPermission.Mentor
                             || p.Permission == MemberPermission.Creator))
                .Join(_context.Users, p => p.MemberId, u => u.Id, (p, u) => u)
                .ToList();
        }

        public ICollection<PeerReviewUser> GetMentors(Guid courseId)
        {
            return _context.Participations
                .Where(p => p.CourseId == courseId &&
                            (p.Permission == MemberPermission.Mentor
                             || p.Permission == MemberPermission.Creator))
                .Join(_context.Users, p => p.MemberId, u => u.Id, (p, u) => u)
                .ToList();
        }

        public bool IsMentor(Guid courseId, Guid memberId)
        {
            return _context.Participations
                .Any(p => p.CourseId == courseId &&
                          p.MemberId == memberId &&
                          (p.Permission & MemberPermission.Mentor) == MemberPermission.Mentor);
        }

        public bool IsMember(Guid courseId, Guid memberId)
        {
            return _context.Participations
                .Any(p => p.CourseId == courseId &&
                          p.MemberId == memberId &&
                          (p.Permission & MemberPermission.Member) == MemberPermission.Member);
        }

        public void MakeMentor(Guid courseId, Guid targetId, Guid executorId)
        {
            var participation = _context.Participations
                .FirstOrDefault(p => p.MemberId == executorId && p.CourseId == courseId);

            if (participation == null
                || participation.Permission == MemberPermission.Invited)
            {
                throw new ArgumentException("No such member");
            }

            if (participation.Permission == MemberPermission.None)
            {
                throw new Exception("Database data exception");
            }

            if (participation.Permission == MemberPermission.Member)
            {
                participation.Permission = MemberPermission.Mentor;
                _context.Participations.Update(participation);
                _context.SaveChanges();
            }
        }

        public void DeleteMember(Guid courseId, Guid targetId, Guid executorId)
        {
            var participation = _context.Participations
                .FirstOrDefault(p => p.MemberId == executorId && p.CourseId == courseId);
            if (participation == null)
            {
                throw new ArgumentException("No such member");
            }

            if (participation.Permission == MemberPermission.None)
            {
                throw new Exception("Database data exception");
            }

            if (participation.Permission == MemberPermission.Member
                || participation.Permission == MemberPermission.Mentor)
            {
                _context.Participations.Remove(participation);
                _context.SaveChanges();
            }
        }
    }
}