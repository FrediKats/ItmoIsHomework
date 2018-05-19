using System;
using System.Collections.Generic;

namespace ReviewYourself.Models
{
    public class ResourceTask
    {
        public Guid Id { get; set; }
        public Guid CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostTime { get; set; }
        //TODO:
        public ICollection<Criteria> CriteriaCollection { get; set; }
    }
}