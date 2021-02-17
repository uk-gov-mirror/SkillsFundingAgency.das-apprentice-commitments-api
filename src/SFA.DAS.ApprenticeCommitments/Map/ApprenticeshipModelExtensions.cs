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
                ApprenticeId = model.ApprenticeId,
                CommitmentsApprenticeshipId = model.CommitmentsApprenticeshipId
            };
        }

        public static ApprenticeshipModel MapToApprenticeshipModel(this Apprenticeship apprenticeship)
        {
            return new ApprenticeshipModel
            {
                Id = apprenticeship.Id,
                ApprenticeId = apprenticeship.ApprenticeId,
                CommitmentsApprenticeshipId = apprenticeship.CommitmentsApprenticeshipId
            };
        }
    }
}
