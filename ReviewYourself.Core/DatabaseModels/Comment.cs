using System;

namespace ReviewYourself.Core.DatabaseModels
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime PostTime { get; set; }

        public Guid AuthorId { get; set; }
        public Member Author { get; set; }
    }
}