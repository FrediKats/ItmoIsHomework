using System;
using System.Collections.Generic;
using System.Data.SqlTypes;

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
        public ICollection<SqlFileStream> AttachmentCollection { get; set; }
    }
}