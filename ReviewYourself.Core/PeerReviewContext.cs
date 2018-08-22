using System.Data.Entity;
using ReviewYourself.Core.DatabaseModels;

namespace ReviewYourself.Core
{
    public class PeerReviewContext : DbContext
    {
        public PeerReviewContext(string connectionString) : base(connectionString)
        {
            
        }

        public DbSet<Announcing> Announcing { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTask> CourseTasks { get; set; }
        public DbSet<Criteria> Criterias { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<MemberType> MemberTypes { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ReviewCriteria> ReviewCriterias { get; set; }
        public DbSet<Solution> Solutions { get; set; }

    }
}