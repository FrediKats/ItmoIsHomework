using System;

namespace ReviewYourself.Models
{
    public class Solution
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid TaskId { get; set; }
        public string TextData { get; set; }
        public DateTime PostTime { get; set; }
        public bool Status { get; set; }
        public byte[][] AttachmentCollection { get; set; }
    }
}