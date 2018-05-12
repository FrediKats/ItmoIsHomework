using System;

namespace ReviewYourself.Models
{
    public class ReviewCriteria
    {
        public Guid ReviewId { get; set; }
        public Guid CriteriaId { get; set; }
        public int Rating { get; set; }
        public string Description { get; set; }

    }
}