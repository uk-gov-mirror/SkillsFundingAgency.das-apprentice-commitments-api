using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeshipRepository
    {
        Task<ApprenticeshipModel> Add(ApprenticeshipModel model);
    }
}