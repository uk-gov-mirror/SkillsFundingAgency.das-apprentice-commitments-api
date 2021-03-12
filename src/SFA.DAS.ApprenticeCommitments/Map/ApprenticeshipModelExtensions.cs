using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeshipModelExtensions
    {
        public static Apprenticeship MapToApprenticeship(this ApprenticeshipModel model)
        {
            return new Apprenticeship
            {
                Id = model.Id ?? 0,
                CommitmentsApprenticeshipId = model.CommitmentsApprenticeshipId,
                EmployerName = model.EmployerName,
                EmployerAccountLegalEntityId = model.EmployerAccountLegalEntityId

            };
        }

        public static ApprenticeshipModel MapToApprenticeshipModel(this Apprenticeship apprenticeship)
        {
            return new ApprenticeshipModel
            {
                Id = apprenticeship.Id,
                CommitmentsApprenticeshipId = apprenticeship.CommitmentsApprenticeshipId,
                EmployerName = apprenticeship.EmployerName,
                EmployerAccountLegalEntityId = apprenticeship.EmployerAccountLegalEntityId
            };
        }
    }
}
