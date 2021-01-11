//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata;

//namespace SFA.DAS.ApprenticeCommitments.Models
//{
//    public class ApprenticeCommitmentsDbContext : DbContext
//    {
//        public ApprenticeCommitmentsDbContext()
//        {
//        }

//        public ApprenticeCommitmentsDbContext(DbContextOptions<ApprenticeCommitmentsDbContext> options) : base(options)
//        {
//        }

//        public virtual DbSet<Registration> Registrations { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.Entity<Registration>(entity =>
//            {
//                entity.Property(e =>e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
//            });

//            base.OnModelCreating(modelBuilder);
//        }
//    }
//}
