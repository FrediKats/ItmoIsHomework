using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ReviewYourself.WebApi.DatabaseModels;
using ReviewYourself.WebApi.Services;

namespace ReviewYourself.WebApi.Controllers
{
    [Route("api/Member")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        [HttpGet("SendInvite/{courseId}/{targetId}")]
        public ActionResult SendStudentInvitation(Guid courseId, Guid targetId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _memberService.SendInvite(courseId, targetId, userId);
            return Ok();
        }

        [HttpGet("AcceptInvite/{courseId}")]
        public ActionResult AcceptInvite(Guid courseId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _memberService.AcceptInvite(courseId, userId);
            return Ok();
        }

        [HttpGet("AcceptInvite/{courseId}")]
        public ActionResult DenyInvite(Guid courseId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _memberService.DenyInvite(courseId, userId);
            return Ok();
        }

        [HttpGet("UserCourses/{userId}")]
        public ActionResult<ICollection<Course>> GetUserCourses(Guid userId)
        {
            return Ok(_memberService.GetUserCourses(userId));
        }


        [HttpGet("UserInvitations/{userId}")]
        public ActionResult<ICollection<Course>> GetUserInvitations(Guid userId)
        {
            return Ok(_memberService.GetUserInvitations(userId));
        }

        [HttpGet("Users/{courseId}")]
        public ActionResult<ICollection<PeerReviewUser>> GetMembers(Guid courseId)
        {
            return Ok(_memberService.GetMembers(courseId));
        }

        [HttpGet("Mentors/{courseId}")]
        public ActionResult<ICollection<PeerReviewUser>> GetMentors(Guid courseId)
        {
            return Ok(_memberService.GetMentors(courseId));
        }

        [HttpGet("IsMentor/{courseId}/{memberId}")]
        public ActionResult<bool> IsMentor(Guid courseId, Guid memberId)
        {
            return _memberService.IsMentor(courseId, memberId);
        }

        [HttpGet("IsMember/{courseId}/{memberId}")]
        public ActionResult<bool> IsMember(Guid courseId, Guid memberId)
        {
            return _memberService.IsMember(courseId, memberId);
        }

        [HttpGet("MakeMentor/{courseId}/{targetId}")]
        public ActionResult MakeMentor(Guid courseId, Guid targetId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _memberService.MakeMentor(courseId, targetId, userId);
            return Ok();
        }

        [HttpGet("MakeMentor/{courseId}/{targetId}")]
        public ActionResult DeleteMember(Guid courseId, Guid targetId)
        {
            Guid userId = Guid.Parse(User.Identity.Name);
            _memberService.DeleteMember(courseId, targetId, userId);
            return Ok();
        }
    }
}