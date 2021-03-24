using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Net.Mail;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class ApprenticeCommitmentsDbContext : DbContext, IRegistrationContext, IApprenticeContext, IApprenticeshipContext
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

        DbSet<Registration> IEntityContext<Registration>.Entities => Registrations;
        DbSet<Apprentice> IEntityContext<Apprentice>.Entities => Apprentices;
        DbSet<Apprenticeship> IEntityContext<Apprenticeship>.Entities => Apprenticeships;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Apprentice>(a =>
            {
                a.ToTable("Apprentice");
                a.HasKey(e => e.Id);
                a.Property(e => e.Email)
                 .HasConversion(
                    v => v.ToString(),
                    v => new MailAddress(v));
                a.OwnsMany(
                    e => e.PreviousEmailAddresses,
                    c =>
                    {
                        c.HasKey("Id");
                        c.Property(typeof(long), "Id");
                        c.HasIndex("ApprenticeId");
                        c.Property(e => e.EmailAddress)
                            .HasConversion(
                                v => v.ToString(),
                                v => new MailAddress(v));
                    });
                a.HasMany(e => e.Apprenticeships).WithOne(a => a.Apprentice);
                a.Property(e => e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<Apprenticeship>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CreatedOn).Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            });

            modelBuilder.Entity<Registration>().OwnsOne(e => e.Details);

            base.OnModelCreating(modelBuilder);
        }
    }
}