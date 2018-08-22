using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

namespace ReviewYourself.Core.DatabaseModels
{
    public class Solution
    {
        public Guid Id { get; set; }
        public string TextData { get; set; }
        public DateTime PostTime { get; set; }
        public bool IsResolved { get; set; }

        public Guid AuthorId { get; set; }
        public Member Author { get; set; }

        public Guid CourseTaskId { get; set; }
        public CourseTask CourseTask { get; set; }
    }
}