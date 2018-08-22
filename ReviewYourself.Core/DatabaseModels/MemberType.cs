using System;
using System.ComponentModel.DataAnnotations.Schema;
using ReviewYourself.Core.Models;

namespace ReviewYourself.Core.DatabaseModels
{
    public class MemberType
    {
        public Guid MemberId { get; set; }
        public Member Member { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }
        
        public MemberPermission Permission { get; set; }
    }
}