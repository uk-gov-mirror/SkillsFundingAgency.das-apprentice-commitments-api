using SFA.DAS.ApprenticeCommitments.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeRepository
    {
        Task<ApprenticeModel> Add(ApprenticeModel model);

        Task ChangeEmailAddress(long apprenticeId, MailAddress email);
    }
}