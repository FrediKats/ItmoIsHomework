using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReviewYourself.UnitTest.Tools;
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
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            var invites = _memberService.GetUserInvitations(otherUser);

            Assert.IsTrue(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void InviteMember_AlreadyInvited()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);

            Assert.ThrowsException<InvalidOperationException>(()
                => _memberService.SendInvite(createdCourse.Id, otherUser, creator));
        }

        [TestMethod]
        public void AcceptInvite_IsMember()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void AcceptInvite_InUsesCourses()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            var courses = _memberService.GetUserCourses(otherUser);

            Assert.IsTrue(courses.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void DenyInvite()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.DenyInvite(createdCourse.Id, otherUser);
            var invites = _memberService.GetUserInvitations(otherUser);

            Assert.IsFalse(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void DenyInvite_ExceptionNoInvite()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            Assert.ThrowsException<Exception>(()
                => _memberService.DenyInvite(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void AcceptInvite_InMemberList()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            var members = _memberService.GetMembers(createdCourse.Id);

            Assert.IsTrue(members.Any(c => c.Id == otherUser));
        }

        [TestMethod]
        public void CreatorIsMentor()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            var members = _memberService.GetMentors(createdCourse.Id);

            Assert.IsTrue(members.Any(c => c.Id == creator));
        }

        [TestMethod]
        public void MakeMentor_IsMentor()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.MakeMentor(createdCourse.Id, otherUser, creator);

            Assert.IsTrue(_memberService.IsMentor(createdCourse.Id, otherUser));
        }

        [TestMethod]
        public void MakeMentor_ImMentorList()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.MakeMentor(createdCourse.Id, otherUser, creator);
            var mentors = _memberService.GetMentors(createdCourse.Id);

            Assert.IsTrue(mentors.Any(m => m.Id == otherUser));
        }

        [TestMethod]
        public void DeleteMember()
        {
            var creator = InstanceFactory.AuthorizedUserId();
            var otherUser = InstanceFactory.AuthorizedUserId();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator);
            _memberService.SendInvite(createdCourse.Id, otherUser, creator);
            _memberService.AcceptInvite(createdCourse.Id, otherUser);
            _memberService.DeleteMember(createdCourse.Id, otherUser, creator);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser));
        }
    }
}