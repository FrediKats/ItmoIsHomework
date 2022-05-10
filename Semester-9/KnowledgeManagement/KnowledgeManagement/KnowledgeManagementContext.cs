using Microsoft.EntityFrameworkCore;

namespace KnowledgeManagement
{
    public class KnowledgeManagementContext : DbContext
    {
        public static string Connection;

        public DbSet<Competency> Competency { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Skill> Skill { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Connection);
        }
    }
}