using System;
using System.Collections.Generic;

namespace ReviewYourself.Core.DatabaseModels
{
    public class Announcing
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostTime { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid AuthorId { get; set; }
        public Member Author { get; set; }
    }
}