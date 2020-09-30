using System;
using System.Collections.Generic;
using System.Linq;

namespace KnowledgeManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            using KnowledgeManagementContext db = new KnowledgeManagementContext();

            List<Skill> roots = db.Skill.Where(s => s.Parent == null).ToList();
            roots.ForEach(s => Console.WriteLine(s.Id));
        }
    }
}
