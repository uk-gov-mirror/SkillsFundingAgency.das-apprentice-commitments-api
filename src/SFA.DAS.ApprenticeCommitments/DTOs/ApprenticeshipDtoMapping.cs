using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.DTOs;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeshipDtoMapping
    {
        public static ApprenticeshipDto? MapToApprenticeshipDto(this Apprenticeship? apprenticeship)
        {
            if (apprenticeship == null) return null;

            return new ApprenticeshipDto
            {
                Id = apprenticeship.Id,
                CommitmentsApprenticeshipId = apprenticeship.CommitmentsApprenticeshipId,
                EmployerName = apprenticeship.Details.EmployerName,
                EmployerAccountLegalEntityId = apprenticeship.Details.EmployerAccountLegalEntityId,
                TrainingProviderId = apprenticeship.Details.TrainingProviderId,
                TrainingProviderName = apprenticeship.Details.TrainingProviderName,
                TrainingProviderCorrect = apprenticeship.TrainingProviderCorrect,
                EmployerCorrect = apprenticeship.EmployerCorrect,
            };
        }
    }
}