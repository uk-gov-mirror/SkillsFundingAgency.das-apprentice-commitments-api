using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Data;

namespace SFA.DAS.ApprenticeCommitments.Infrastructure
{
    public class ApprenticeCommitmentsHealthCheck : IHealthCheck
    {
        private const string HealthCheckResultsDescription = "Apprentice Commitments API Health Check";
        private readonly IRegistrationRepository _registrationRepository;

        public ApprenticeCommitmentsHealthCheck(IRegistrationRepository registrationRepository)
        {
            _registrationRepository = registrationRepository;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy(HealthCheckResultsDescription)); //, new Dictionary<string, object>());
        }
    }
}
