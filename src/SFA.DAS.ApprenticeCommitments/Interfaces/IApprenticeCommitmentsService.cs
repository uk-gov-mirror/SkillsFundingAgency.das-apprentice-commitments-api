using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SFA.DAS.ApprenticeCommitments.Interfaces
{
    public interface IApprenticeCommitmentsService
    {
        Task<bool> IsHealthy();
    }
}
