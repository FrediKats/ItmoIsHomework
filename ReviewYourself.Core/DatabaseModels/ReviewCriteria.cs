using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewYourself.Core.DatabaseModels
{
    public class ReviewCriteria
    {
        public Guid ReviewId { get; set; }
        public Review Review { get; set; }

        public Guid CriteriaId { get; set; }
        public Criteria Criteria { get; set; }

        public int Rating { get; set; }
        public string Description { get; set; }

    }
}