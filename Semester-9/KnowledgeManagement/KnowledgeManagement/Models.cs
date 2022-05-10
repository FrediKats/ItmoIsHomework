using System.ComponentModel.DataAnnotations.Schema;

namespace KnowledgeManagement
{
    [Table("COMPETENCY")]
    public class Competency
    {
        public int Id { get; set; }
        public int Skill { get; set; }
        [ForeignKey("Skill")]
        public Skill SkillEntity { get; set; }
        public int Level { get; set; }


        public int Owner { get; set; }
        [ForeignKey("Owner")]
        public Profile OwnerEntity { get; set; }
    }

    [Table("PROFILE")]
    public class Profile
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Title_RU { get; set; }
        public int Price { get; set; }
    }

    [Table("SKILL")]
    public class Skill
    {
        public int Id { get; set; }
        public string About { get; set; }
        public string About_RU { get; set; }
        public int MaxLevel { get; set; }
        public string Name { get; set; }
        public string Name_RU { get; set; }
        public int? Parent { get; set; }
    }
}