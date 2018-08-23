using System;
using System.Collections.Generic;

namespace ReviewYourself.WebApi.DatabaseModels
{
    public class Review
    {
        public Guid Id { get; set; }
        public DateTime PostTime { get; set; }
        public ICollection<ReviewCriteria> Evaluations { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public Guid SolutionId { get; set; }
        public Solution Solution { get; set; }
    }
}