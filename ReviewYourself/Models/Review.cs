using System;

namespace ReviewYourself.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SolutionId { get; set; }
        public DateTime PostTime { get; set; }
    }
}