using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeshipModelExtensions
    {
        public static ApprenticeshipDto? MapToApprenticeshipModel(this Apprenticeship? apprenticeship)
        {
            if (apprenticeship == null) return null;

            return new ApprenticeshipDto
            {
                Id = apprenticeship.Id,
                CommitmentsApprenticeshipId = apprenticeship.CommitmentsApprenticeshipId,
                EmployerName = apprenticeship.EmployerName,
                EmployerAccountLegalEntityId = apprenticeship.EmployerAccountLegalEntityId,
                TrainingProviderId = apprenticeship.TrainingProviderId,
                TrainingProviderName = apprenticeship.TrainingProviderName,
                TrainingProviderCorrect = apprenticeship.TrainingProviderCorrect,
            };
        }
    }
}