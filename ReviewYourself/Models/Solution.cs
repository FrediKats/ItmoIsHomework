using System;
using System.Collections.Generic;

namespace ReviewYourself.Models
{
    public class Solution
    {
        public Guid Id { get; set; }
        public Guid AuthorId { get; set; }
        public Guid TaskId { get; set; }
        public string TextData { get; set; }
        public DateTime PostTime { get; set; }
        public bool IsResolved { get; set; }
        //TODO: far away future
        public ICollection<byte[]> AttachmentCollection { get; set; }
    }
}