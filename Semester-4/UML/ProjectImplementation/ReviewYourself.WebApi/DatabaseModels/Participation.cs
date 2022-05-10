using System;
using ReviewYourself.WebApi.Models;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Participation
    {
        public Guid MemberId { get; set; }
        public PeerReviewUser Member { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public MemberPermission Permission { get; set; }
    }
}