using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure
{
    public class ApprenticeCommitmentsHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultsDescription = "Apprentice Commitments API Health Check";


        public ApprenticeCommitmentsHealthCheck()
        {

        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy(HealthCheckResultsDescription)); //, new Dictionary<string, object>());
        }
    }
}
