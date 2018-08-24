using System.Linq;
using Microsoft.EntityFrameworkCore;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Tools
{
    public class PeerReviewContext : DbContext
    {
        public PeerReviewContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Announcing> Announcing { get; set; }
        public DbSet<AuthorizeData> AuthorizeDatas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTask> CourseTasks { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<User> Members { get; set; }
        public DbSet<Participation> MemberTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewCriteria> ReviewCriterias { get; set; }
        public DbSet<Solution> Solutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participation>()
                .HasKey(mt => new {mt.CourseId, mt.MemberId});

            modelBuilder.Entity<ReviewCriteria>()
                .HasKey(rc => new {rc.CriteriaId, rc.ReviewId});

            //TODO: fix cascade delete
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}