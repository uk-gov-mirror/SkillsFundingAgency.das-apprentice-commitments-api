using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeshipModelExtensions
    {
        public static Apprenticeship MapToApprenticeship(this ApprenticeshipModel model)
            => model.MapToApprenticeship(new Apprenticeship { Id = model.Id ?? 0 });

        public static Apprenticeship MapToApprenticeship(this ApprenticeshipModel model, Apprenticeship apprenticeship)
        {
            apprenticeship.CommitmentsApprenticeshipId = model.CommitmentsApprenticeshipId;
            apprenticeship.EmployerName = model.EmployerName;
            apprenticeship.EmployerAccountLegalEntityId = model.EmployerAccountLegalEntityId;
            apprenticeship.TrainingProviderId = model.TrainingProviderId;
            apprenticeship.TrainingProviderName = model.TrainingProviderName;
            apprenticeship.TrainingProviderCorrect = model.TrainingProviderCorrect;
            return apprenticeship;
        }

        public static ApprenticeshipModel MapToApprenticeshipModel(this Apprenticeship apprenticeship)
        {
            return new ApprenticeshipModel
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