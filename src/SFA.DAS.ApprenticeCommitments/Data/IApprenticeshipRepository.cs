using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.ApprenticeCommitments.Models;

namespace SFA.DAS.ApprenticeCommitments.Data
{
    public interface IApprenticeshipRepository
    {
        Task<ApprenticeshipModel> Add(ApprenticeshipModel model);
        Task<List<ApprenticeshipModel>> FindByApprenticeId(Guid apprenticeId);
        Task<ApprenticeshipModel> Get(Guid apprenticeId, long apprenticeshipId);
    }
}