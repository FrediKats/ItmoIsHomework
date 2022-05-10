using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Spectre.Console;

namespace KnowledgeManagement
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            KnowledgeManagementContext.Connection = String.Empty;
            using KnowledgeManagementContext db = new KnowledgeManagementContext();

            List<Skill> roots = db.Skill.Where(s => s.Parent == null).ToList();
            List<int> rootsId = roots.Select(s => s.Id).ToList();
            List<Skill> secondLevelSkills = db.Skill.Where(s => s.Parent != null).Where(s => rootsId.Contains(s.Parent.Value)).ToList();

            secondLevelSkills.ForEach(s => PrintRoot(db, s));
        }

        private static void PrintRoot(KnowledgeManagementContext db, Skill rootSkill)
        {
            List<int> childSkills = RecursiveSearch(db, new List<int> { rootSkill.Id })
                .Select(s => s.Id)
                .ToList();

            List<Profile> profiles = db.Competency
                .Include(c => c.OwnerEntity)
                .Where(c => childSkills.Contains(c.Skill))
                .Select(c => c.OwnerEntity)
                .ToList()
                .DistinctBy(p => p.Id)
                .ToList();

            AnsiConsole.WriteLine($"Second level skill: {rootSkill.Id}. {rootSkill.Name_RU}, Child skills: {childSkills.Count}, Child profiles: {profiles.Count}");
            var table = new Table();
            table.AddColumn(new TableColumn("[u]Id[/]"));
            table.AddColumn(new TableColumn("[u]Title[/]"));
            table.AddColumn(new TableColumn("[u]Price[/]"));

            foreach (Profile user in profiles)
                table.AddRow(user.Id.ToString(), user.Title ?? user.Title_RU, user.Price.ToString());

            AnsiConsole.Render(table);
        }

        private static List<Skill> RecursiveSearch(KnowledgeManagementContext db, List<int> roots)
        {
            List<Skill> skills = db.Skill.Where(s => s.Parent != null && roots.Contains(s.Parent.Value)).ToList();

            List<int> nextSearch = skills.Select(s => s.Id).ToList();
            if (nextSearch.Any())
                skills.AddRange(RecursiveSearch(db, nextSearch));

            return skills;
        }
    }
}
