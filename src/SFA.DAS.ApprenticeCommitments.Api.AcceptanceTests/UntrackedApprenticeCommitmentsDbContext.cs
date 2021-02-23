using Microsoft.EntityFrameworkCore;
using SFA.DAS.ApprenticeCommitments.Data.Models;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public class UntrackedApprenticeCommitmentsDbContext : ApprenticeCommitmentsDbContext
    {
        public UntrackedApprenticeCommitmentsDbContext(DbContextOptions<ApprenticeCommitmentsDbContext> options) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTrackingWithIdentityResolution;
        }

        public override int SaveChanges()
        {
            var r = base.SaveChanges();
            ChangeTracker.Clear();
            return r;
        }
    }
}