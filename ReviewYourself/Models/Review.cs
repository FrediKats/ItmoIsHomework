using System;
using System.Collections.Generic;

namespace ReviewYourself.Models
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid SolutionId { get; set; }
        public DateTime PostTime { get; set; }
        public ICollection<ReviewCriteria> RateCollection { get; set; }
    }
}