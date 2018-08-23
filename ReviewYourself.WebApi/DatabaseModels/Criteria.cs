using System;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Criteria
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxPoint { get; set; }
        
        public Guid CourseTaskId { get; set; }
        public CourseTask CourseTask { get; set; }
    }
}