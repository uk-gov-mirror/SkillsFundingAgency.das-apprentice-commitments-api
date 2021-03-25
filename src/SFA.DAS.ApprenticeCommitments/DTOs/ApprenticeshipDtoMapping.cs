using SFA.DAS.ApprenticeCommitments.Data.Models;

#nullable enable

namespace SFA.DAS.ApprenticeCommitments.DTOs
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
                EmployerName = apprenticeship.EmployerName,
                EmployerAccountLegalEntityId = apprenticeship.EmployerAccountLegalEntityId,
                TrainingProviderId = apprenticeship.TrainingProviderId,
                TrainingProviderName = apprenticeship.TrainingProviderName,
                TrainingProviderCorrect = apprenticeship.TrainingProviderCorrect,
                EmployerCorrect = apprenticeship.EmployerCorrect,
            };
        }
    }
}