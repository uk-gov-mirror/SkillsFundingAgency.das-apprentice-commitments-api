using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeModelExtensions
    {
        public static Apprentice MapToApprentice(this ApprenticeModel apprenticeModel)
        {
            return new Apprentice(
                apprenticeModel.UserIdentityId,
                apprenticeModel.FirstName,
                apprenticeModel.LastName,
                apprenticeModel.Email,
                apprenticeModel.DateOfBirth
                );
        }

        public static ApprenticeModel MapToApprenticeModel(this Apprentice apprentice)
        {
            return new ApprenticeModel
            {
                Id = apprentice.Id,
                UserIdentityId = apprentice.UserIdentityId,
                FirstName = apprentice.FirstName,
                LastName = apprentice.LastName,
                Email = apprentice.Email,
                DateOfBirth = apprentice.DateOfBirth
            };
        }
    }
}
