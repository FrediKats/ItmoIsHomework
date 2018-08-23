using System;
using System.Collections.Generic;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ICollection<Participation> Members { get; set; }
        public ICollection<CourseTask> CourseTasks { get; set; }
        public ICollection<Announcing> Announcings { get; set; }
    }
}