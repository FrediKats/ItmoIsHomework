using System;
using System.Collections.Generic;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class CourseTask
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostTime { get; set; }
        //TODO:
        public ICollection<Criteria> Criterias { get; set; }
        public ICollection<Solution> Solutions { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Guid CourseId { get; set; }
        public Course Course { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }
    }
}