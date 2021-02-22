using SFA.DAS.ApprenticeCommitments.Models;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeRepository
    {
        Task<ApprenticeModel> Add(ApprenticeModel model);

        Task ChangeEmailAddress(long apprenticeId, string email);
    }
}