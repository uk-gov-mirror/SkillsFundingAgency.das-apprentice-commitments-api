using SFA.DAS.ApprenticeCommitments.Models;
using System;
using System.Net.Mail;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeRepository
    {
        Task<ApprenticeModel> AddApprentice(
            ApprenticeModel apprentice, ApprenticeshipModel firstApprenticeship);

        Task ChangeEmailAddress(Guid apprenticeId, MailAddress email);
    }
}