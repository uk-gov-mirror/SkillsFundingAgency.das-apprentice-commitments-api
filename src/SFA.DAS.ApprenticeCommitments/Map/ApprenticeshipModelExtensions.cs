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
                Organisation = model.Organisation
            };
        }

        public static ApprenticeshipModel MapToApprenticeshipModel(this Apprenticeship apprenticeship)
        {
            return new ApprenticeshipModel
            {
                Id = apprenticeship.Id,
                CommitmentsApprenticeshipId = apprenticeship.CommitmentsApprenticeshipId,
                Organisation = apprenticeship.Organisation
            };
        }
    }
}
