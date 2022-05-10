using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ReviewYourself.WebApi.DatabaseModels;

namespace ReviewYourself.WebApi.Tools
{
    public class PeerReviewContext : DbContext
    {
        public PeerReviewContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Announcing> Announcing { get; set; }
        public DbSet<AuthData> AuthorizeDatas { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTask> CourseTasks { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Participation> Participations { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewCriteria> ReviewCriterias { get; set; }
        public DbSet<CourseSolution> Solutions { get; set; }
        public DbSet<PeerReviewUser> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Participation>()
                .HasKey(mt => new {mt.CourseId, mt.MemberId});
            modelBuilder.Entity<ReviewCriteria>()
                .HasKey(rc => new {rc.CriteriaId, rc.ReviewId});
            modelBuilder.Entity<PeerReviewUser>()
                .HasIndex(u => u.Login)
                .IsUnique();

            //TODO: fix cascade delete
            IEnumerable<IMutableForeignKey> cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (IMutableForeignKey fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}