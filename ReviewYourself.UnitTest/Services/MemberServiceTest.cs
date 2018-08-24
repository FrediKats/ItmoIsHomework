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
            _courseService = ServiceFactory.CourseService();
            _memberService = ServiceFactory.MemberService();
        }

        [TestMethod]
        public void InviteMember()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMemberInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            var invites = _memberService.GetUserInvitations(otherUser.UserId);

            Assert.IsTrue(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void InviteMentor()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            var invites = _memberService.GetUserInvitations(otherUser.UserId);

            Assert.IsTrue(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void AcceptInvite_IsMember()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser.UserId));
        }

        [TestMethod]
        public void AcceptInvite_InUsesCourses()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);
            var courses = _memberService.GetUserCourses(otherUser.UserId);

            Assert.IsTrue(courses.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void DenyInvite_NoInvite()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.DenyInvite(createdCourse.Id, otherUser.UserId);
            var invites = _memberService.GetUserInvitations(otherUser.UserId);

            Assert.IsTrue(invites.Any(c => c.Id == createdCourse.Id));
        }

        [TestMethod]
        public void AcceptInvite_InMemberList()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);
            var members = _memberService.GetMembers(otherUser.UserId);

            Assert.IsTrue(members.Any(c => c.Id == otherUser.UserId));
        }

        [TestMethod]
        public void CreatorIsMentor()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            var members = _memberService.GetMentors(creator.UserId);

            Assert.IsTrue(members.Any(c => c.Id == creator.UserId));
        }

        [TestMethod]
        public void MakeMentor_IsMentor()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);
            _memberService.MakeMentor(createdCourse.Id, otherUser.UserId, creator.UserId);

            Assert.IsTrue(_memberService.IsMentor(createdCourse.Id, otherUser.UserId));
        }

        [TestMethod]
        public void MakeMentor_ImMentorList()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);
            _memberService.MakeMentor(createdCourse.Id, otherUser.UserId, creator.UserId);
            var mentors = _memberService.GetMentors(createdCourse.Id);

            Assert.IsTrue(mentors.Any(m => m.Id == otherUser.UserId));
        }

        [TestMethod]
        public void DeleteMember_IsNotMember()
        {
            var creator = InstanceFactory.RegisteredUserToken();
            var otherUser = InstanceFactory.RegisteredUserToken();
            var course = InstanceFactory.Course();

            var createdCourse = _courseService.Create(course, creator.UserId);
            _memberService.SendMentorInvitation(createdCourse.Id, otherUser.UserId, creator.UserId);
            _memberService.AcceptInvite(createdCourse.Id, otherUser.UserId);
            _memberService.DeleteMember(createdCourse.Id, otherUser.UserId, creator.UserId);

            Assert.IsTrue(_memberService.IsMember(createdCourse.Id, otherUser.UserId));
        }
    }
}