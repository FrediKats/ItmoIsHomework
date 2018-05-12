using System;

namespace ReviewYourself.Models
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public ResourceUser Mentor { get; set; }
    }
}