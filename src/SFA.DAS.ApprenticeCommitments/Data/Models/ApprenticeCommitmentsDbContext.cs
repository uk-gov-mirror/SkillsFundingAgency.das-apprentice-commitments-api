using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class ApprenticeCommitmentsDbContext : DbContext
    {
        public ApprenticeCommitmentsDbContext()
        {
        }

        public ApprenticeCommitmentsDbContext(DbContextOptions<ApprenticeCommitmentsDbContext> options) : base(options)
        {
        }

        public virtual DbSet<Registration> Registrations { get; set; }
        public virtual DbSet<Apprentice> Apprentices { get; set; }
        public virtual DbSet<Apprenticeship> Apprenticeships { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apprentice>()
                .ToTable("Apprentice")
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Apprenticeship>()
                .ToTable("Apprenticeship")
                .Property(a => a.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.Property(e =>e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
