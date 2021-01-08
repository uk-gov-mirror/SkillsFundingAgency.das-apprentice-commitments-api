using SFA.DAS.ApprenticeCommitments.Infrastructure.MediatorExtensions;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.RegisterAccountCommand
{
    public class RegisterAccountCommand : ITransactionCommand
    {
        public long ApprenticeshipId { get; set; }
        public string Email { get; set; }
    }
}
