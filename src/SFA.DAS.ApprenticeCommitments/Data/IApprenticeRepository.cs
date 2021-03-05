using SFA.DAS.ApprenticeCommitments.Models;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeRepository
    {
        Task<ApprenticeModel> AddApprentice(
            ApprenticeModel apprentice, ApprenticeshipModel firstApprenticeship);

        Task ChangeEmailAddress(long apprenticeId, MailAddress email);
    }
}