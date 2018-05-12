using System;

namespace ReviewYourself.Models
{
    public class Criteria
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int MaxPoint { get; set; }
    }
}