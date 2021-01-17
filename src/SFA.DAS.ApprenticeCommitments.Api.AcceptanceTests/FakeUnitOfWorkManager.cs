using System;
using System.Threading.Tasks;
using SFA.DAS.UnitOfWork.Managers;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public class FakeUnitOfWorkManager : IUnitOfWorkManager
    {
        public Task BeginAsync()
        {
            return Task.CompletedTask;
        }

        public Task EndAsync(Exception ex = null)
        {
            return Task.CompletedTask;
        }
    }
}