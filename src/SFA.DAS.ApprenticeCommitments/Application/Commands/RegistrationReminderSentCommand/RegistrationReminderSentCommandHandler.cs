using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.ApprenticeCommitments.Data;

namespace SFA.DAS.ApprenticeCommitments.Application.Commands.RegistrationReminderSentCommand
{
    public class RegistrationReminderSentCommandHandler : IRequestHandler<RegistrationReminderSentCommand>
    {
        private readonly IRegistrationContext _registrations;

        public RegistrationReminderSentCommandHandler(IRegistrationContext registrations)
        {
            _registrations = registrations;
        }

        public async Task<Unit> Handle(RegistrationReminderSentCommand command, CancellationToken _)
        {
            var registration = await _registrations.GetById(command.ApprenticeId);
            registration.SignUpReminderSent(command.SentOn);
            return Unit.Value;
        }
    }
}