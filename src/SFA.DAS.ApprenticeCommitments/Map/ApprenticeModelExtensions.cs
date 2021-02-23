using SFA.DAS.ApprenticeCommitments.Data.Models;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Map
{
    public static class ApprenticeModelExtensions
    {
        public static Apprentice MapToApprentice(this ApprenticeModel apprenticeModel)
        {
            return new Apprentice(
                apprenticeModel.FirstName,
                apprenticeModel.LastName,
                apprenticeModel.UserIdentityId,
                apprenticeModel.Email,
                apprenticeModel.DateOfBirth
                );
        }

        public static ApprenticeModel MapToApprenticeModel(this Apprentice apprentice)
        {
            return new ApprenticeModel
            {
                Id = apprentice.Id,
                FirstName = apprentice.FirstName,
                LastName = apprentice.LastName,
                UserIdentityId = apprentice.UserIdentityId,
                Email = apprentice.Email,
                DateOfBirth = apprentice.DateOfBirth
            };
        }
    }
}
