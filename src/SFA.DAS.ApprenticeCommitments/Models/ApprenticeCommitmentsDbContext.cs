using Microsoft.EntityFrameworkCore;

namespace SFA.DAS.ApprenticeCommitments.Models
{
    public class ApprenticeCommitmentsDbContext : DbContext
    {
        public ApprenticeCommitmentsDbContext()
        {
            
        }

        public ApprenticeCommitmentsDbContext(DbContextOptions<ApprenticeCommitmentsDbContext> options) : base(options)
        {
            
        }
    }
}
