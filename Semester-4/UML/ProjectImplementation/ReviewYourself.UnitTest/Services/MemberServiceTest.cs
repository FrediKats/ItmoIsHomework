using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.UnitTest.Services
{
    [TestClass]
    public class MemberServiceTest
    {
        private ICourseService _courseService;
        private IMemberService _memberService;

        [TestInitialize]
        public void Init()
        {
            _courseService = ServiceFactory.CourseService;
            _memberService = ServiceFactory.MemberService;
        }

        [TestMethod]
        public void InviteMember()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            ICollection<Course> invites = _memberService.GetUserInvitations(otherUser);

            Assert.IsTrue(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void InviteMember_AlreadyInvited()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);

            Assert.ThrowsException<InvalidOperationException>(()
                => _memberService.SendInvite(createdCourse.Id, otherUser, creator));
        }

        [TestMethod]
        public void AcceptInvite_IsMember()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void AcceptInvite_InUsesCourses()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            ICollection<Course> courses = _memberService.GetUserCourses(otherUser);

            Assert.IsTrue(courses.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void DenyInvite()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.DenyInvite(createdCourse.Id, otherUser);
            ICollection<Course> invites = _memberService.GetUserInvitations(otherUser);

            Assert.IsFalse(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void DenyInvite_ExceptionNoInvite()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            Assert.ThrowsException<ArgumentException>(()
                => _memberService.DenyInvite(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void AcceptInvite_InMemberList()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            ICollection<PeerReviewUser> members = _memberService.GetMembers(createdCourse.Id);

            Assert.IsTrue(members.Any(c => c.Id == otherUser));
        }

        [TestMethod]
        public void CreatorIsMentor()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            ICollection<PeerReviewUser> members = _memberService.GetMentors(createdCourse.Id);

            Assert.IsTrue(members.Any(c => c.Id == creator));
        }

        [TestMethod]
        public void MakeMentor_IsMentor()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.MakeMentor(createdCourse.Id, otherUser, creator);

            Assert.IsTrue(_memberService.IsMentor(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void MakeMentor_ImMentorList()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.MakeMentor(createdCourse.Id, otherUser, creator);
            ICollection<PeerReviewUser> mentors = _memberService.GetMentors(createdCourse.Id);

            Assert.IsTrue(mentors.Any(m => m.Id == otherUser));
        }

        [TestMethod]
        public void DeleteMember()
        {
            Guid creator = InstanceFactory.AuthorizedUserId();
            Guid otherUser = InstanceFactory.AuthorizedUserId();
            Course course = InstanceFactory.Course();

            Course createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.DeleteMember(createdCourse.Id, otherUser, creator);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser));
        }
    }
}