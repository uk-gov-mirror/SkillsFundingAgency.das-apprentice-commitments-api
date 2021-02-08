using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeRepository
    {
        Task<ApprenticeModel> Add(ApprenticeModel model);
    }
}