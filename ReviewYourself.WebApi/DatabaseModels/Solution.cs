using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Solution
    {
        public Guid Id { get; set; }
        public string TextData { get; set; }
        public DateTime PostTime { get; set; }
        public bool IsResolved { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }


        public Guid CourseTaskId { get; set; }
        public CourseTask CourseTask { get; set; }
    }
}